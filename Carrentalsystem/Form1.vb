
Public Class Mainform

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btncustomers.Click
        customers.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btncars.Click
        Cars.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnexit.Click
        Me.Close()
    End Sub

    Private Sub BookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookToolStripMenuItem.Click
        Booking.Show()
    End Sub


    Private Sub RentDetailsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RentDetailsToolStripMenuItem1.Click
        rentdetails.Show()
    End Sub

    Private Sub Btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnlogin.Click
        Login.Show()
        If Btnlogin.Text = "Log Out" Then
            Login.Show()
            Btnlogin.Text = ""
            Label2.Text = Nothing
            Label3.Text = Nothing
            Booking.Txtprocess.Text = ""
            Btncustomers.Enabled = False
            Btncars.Enabled = False
            Btnexit.Enabled = False
            MenuStrip1.Enabled = False
        End If
    End Sub

    Private Sub btnadmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Addadmin.Show()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub RentACarBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RentACarBookToolStripMenuItem.Click

    End Sub

    Private Sub RentDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RentDetailsToolStripMenuItem.Click

    End Sub
End Class
