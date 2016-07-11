Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class Main

    Private Function getMemberID(ByVal username)

        Dim memberID As String
        Dim regex As Regex = New Regex("(?:Member ID: (\w*))")
        Dim sourceString As String = New WebClient().DownloadString("http://pc.3dsplaza.com/members/view_profile.php?user=" & username)
        Dim matches As MatchCollection = regex.Matches(sourceString)

        For Each match As Match In matches
            memberID = match.Groups(1).Value
        Next

        If matches.Count = 0 Then
            memberID = "Nothing"
        End If

        Return memberID

    End Function

    Private Function getUnique(ByVal profileUsername)

        Dim uniqueID As String
        Dim regex As Regex = New Regex("(?:extrs = ')(\w*)")
        Dim sourceString As String = New WebClient().DownloadString("http://pc.3dsplaza.com/members/view_profile.php?user=" & profileUsername)
        Dim matches As MatchCollection = regex.Matches(sourceString)

        For Each match As Match In matches
            uniqueID = match.Groups(1).Value
        Next

        Return uniqueID

    End Function

    Private Function getCommentIDs(ByVal profileUsername, ByVal pageNumber)

        Dim memberID As String = getMemberID(profileUsername)
        Dim unique As String = getUnique(profileUsername)

        Dim commentIDs As New ArrayList
        Dim regex As Regex = New Regex("(?:\<div id='comment_(\w*))")
        Dim sourceString As String = New WebClient().DownloadString("http://pc.3dsplaza.com/comment_system/fetch_comments.php?page=" _
            & pageNumber & "&&id=" _
            & memberID & "&&t=profile_comments&&u=ledengegevens&&sp=profiles&&extrs=" _
            & unique)

        Dim matches As MatchCollection = regex.Matches(sourceString)

        For Each match As Match In matches
            commentIDs.Add(match.Groups(1).Value)
        Next

        Return commentIDs

    End Function

    Private Function getCommentUsernames(ByVal profileUsername, ByVal pageNumber)

        Dim memberID As String = getMemberID(profileUsername)
        Dim unique As String = getUnique(profileUsername)

        Dim commentUsernames As New ArrayList
        Dim regex As Regex = New Regex("(?:\?user=(\w*))")
        Dim sourceString As String = New WebClient().DownloadString("http://pc.3dsplaza.com/comment_system/fetch_comments.php?page=" _
            & pageNumber & "&&id=" _
            & memberID & "&&t=profile_comments&&u=ledengegevens&&sp=profiles&&extrs=" _
            & unique)

        Dim matches As MatchCollection = regex.Matches(sourceString)

        For Each match As Match In matches
            commentUsernames.Add(match.Groups(1).Value)
        Next

        Return commentUsernames

    End Function

    Private Function generateChecktime()

        Dim randomizer As Random = New Random
        Dim checktime As Double = randomizer.NextDouble()

        Return checktime

    End Function

    Private Function validateArguments(ByVal s As String)

        Dim valid As Boolean

        Try
            If Integer.TryParse(s, Integer.Parse(s)) = True Then
                valid = True
                Return valid
            End If
        Catch
            valid = False
            Return valid
        End Try

    End Function

    Private Sub sendRequest(ByVal commentID As String, ByVal memberID As String, ByVal unique As String, ByVal message As String)

        ' CHECK IF MEMBER ID IS GIVEN
        If memberID = "Nothing" Then
            MsgBox("Comment could not be edited, user is banned")
            Exit Sub
        End If

        ' VALIDATE GIVEN ARGUMENTS
        If validateArguments(commentID) = False Then
            MsgBox("Invalid Comment ID: Not an integer")
        End If
        If validateArguments(memberID) = False Then
            MsgBox("Invalid Member ID: Not an integer")
        End If

        'PREPARE REQUEST
        Dim dataStream As Stream
        Dim response As HttpWebResponse

        Dim web As HttpWebRequest = WebRequest.Create("http://pc.3dsplaza.com/comment_system/edit_comment.php?id=" _
                                    & commentID _
                                    & "&&t=profile_comments&&u=ledengegevens&&sp=profiles&&extrs=" & unique _
                                    & "&&s=" & generateChecktime())

        Dim cookies = New CookieContainer

        web.Method = "POST"
        web.ContentType = "application/x-www-form-urlencoded"
        web.CookieContainer = cookies
        web.CookieContainer.Add(New Uri("http://pc.3dsplaza.com"),
                                New Cookie("member_id", memberID))
        web.CookieContainer.Add(New Uri("http://pc.3dsplaza.com"),
                                New Cookie("wachtwoord", "garbagestring"))

        web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0"

        Dim postData As String = "message=" & WebUtility.HtmlEncode(message) & "&&checktime=" & generateChecktime()
        Dim postBytes As Byte() = Encoding.UTF8.GetBytes(postData)
        web.ContentLength = postBytes.Length

        dataStream = web.GetRequestStream()
        dataStream.Write(postBytes, 0, postBytes.Length)
        dataStream.Close()

        response = web.GetResponse()
        'MsgBox(CType(response, HttpWebResponse).StatusDescription)

        dataStream = response.GetResponseStream()
        Dim dataReader As New StreamReader(dataStream)
        'MsgBox("Response From Server: " & dataReader.ReadToEnd())

        dataReader.Close()
        dataStream.Close()
        response.Close()

    End Sub

    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click

        Dim profileUsername As String = textboxProfileUsername.Text

        Dim currentCommentIndex As Int32 = 0
        Dim commentUsernames As New ArrayList
        Dim commentIDs As New ArrayList

        For Each username In getCommentUsernames(profileUsername, "1")
            commentUsernames.Add(username)
        Next

        For Each id In getCommentIDs(profileUsername, "1")
            commentIDs.Add(id)
        Next

        While currentCommentIndex < 10

            If commentUsernames.Item(currentCommentIndex) = "Nothing" Then
                MsgBox("User (" & commentUsernames.Item(currentCommentIndex) & ") is banned, cannot edit comment.")
            Else
                Dim currentID As String = commentIDs.Item(currentCommentIndex)
                Dim currentUsername As String = commentUsernames.Item(currentCommentIndex)
                Dim currentMemberID As String = getMemberID(currentUsername)
                Dim currentUnique As String = getUnique(profileUsername)
                Dim message As String = textboxMessage.Text

                Dim webThread As Thread = New Thread(Sub() sendRequest(currentID, currentMemberID, currentUnique, message))
                webThread.Start()

            End If

            currentCommentIndex = currentCommentIndex + 1
        End While

        MsgBox("Done!")

    End Sub
End Class
