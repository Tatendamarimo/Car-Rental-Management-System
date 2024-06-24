Public Class Addadmin

    Private Sub Btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnclose.Click
        Me.Close()
    End Sub

    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        Try
            Sql = "INSERT INTO tbladmin (ADMIN_USER,ADMIN_PASS) VALUES('" & txtuser.Text & "','" & Txtpass.Text & "')"
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New admin record has been saved!")
                conn.Close()

            Else
                MsgBox("No admin record has been saved!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub chkshowpass_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkshowpass.CheckedChanged
        If chkshowpass.Checked Then
            Txtpass.UseSystemPasswordChar = False
        Else
            Txtpass.UseSystemPasswordChar = True
        End If
    End Sub
End Class