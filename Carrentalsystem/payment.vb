Public Class payment
    Dim cmd As New OleDb.OleDbCommand

    Private Sub txtamountreceived_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtamountreceived.TextChanged
        txtchange.Text = Val(txtamountreceived.Text) - Val(txttotal.Text)
    End Sub

    Private Sub txtchange_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtchange.TextChanged
        If Val(txtchange.Text) < 0 Then
            txtchange.Text = 0
        End If
    End Sub




    Private Sub Btnokay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnokay.Click
        Dim carid As Integer = Getsinglevalue("SELECT CAR_ID AS ID FROM tblcar WHERE CAR_NAME ='" & Booking.Cboxcarname.Text & "'")
        Try
            Sql = "UPDATE tblcar SET STATUS = 'Unavailable' WHERE CAR_ID = " & carid
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("Car status has now been updated!")
                conn.Close()

            Else
                MsgBox("Nothing has been updated!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        Me.Close()

    End Sub
End Class