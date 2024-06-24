Imports System.Data.OleDb
Public Class Login

    Private Sub Btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnlogin.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Carrental.accdb")
        Dim cmd As OleDbCommand = New OleDbCommand( _
                   "SELECT * FROM tbladmin WHERE ADMIN_USER = '" & _
                   Txtuser.Text & "' AND [ADMIN_PASS] = '" & Txtpass.Text & "' ", con)
        Dim user As String = ""
        Dim pass As String = ""

        con.Open()

        Dim sdr As OleDbDataReader = cmd.ExecuteReader()
        If (sdr.Read() = True) Then
            user = sdr("ADMIN_USER")
            pass = sdr("ADMIN_PASS")
            Dim unused = MessageBox.Show("Welcome sir/Madam!")
            Me.Close()
            Mainform.Label2.Text = "Hello"
            Mainform.Label3.Text = (user)
            Booking.Txtprocess.Text = user
            Mainform.MenuStrip1.Enabled = True
            Mainform.Btncustomers.Enabled = True
            Mainform.Btncars.Enabled = True
            Mainform.Btnexit.Enabled = True
        Else
            MessageBox.Show("Invalid username or password!")
        End If
        Mainform.Btnlogin.Text = "Log Out"
 

    End Sub

    Private Sub Btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnclose.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Addadmin.Show()
    End Sub

    Private Sub Txtuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtuser.KeyDown, Txtpass.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btnlogin_Click(sender, e)
        End If
    End Sub

End Class