Imports System.Data.OleDb
Imports System.IO
Public Class Cars
    Dim conn As New OleDb.OleDbConnection
    Dim Myconnection As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Carrental.accdb")
    Dim cmd As New OleDb.OleDbCommand
    Dim da As New OleDb.OleDbDataAdapter
    Dim result As Integer
    Dim imgpath As String
    Dim arrImage() As Byte
    Dim sql As String
    Private Sub Btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnload.Click
        dbdt = New DataTable
        Try
            sql = "SELECT * FROM tblcar"
            conn.ConnectionString = Myconnection
            conn.Open()
            With dbcmd
                .CommandText = sql
                .Connection = conn
            End With

            dbda.SelectCommand = dbcmd
            dbda.Fill(dbdt)
            DataGridView1.DataSource = dbdt


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub
    Public Sub cleartextfields()
        For Each crt As Control In GroupBox2.Controls
            If crt.GetType Is GetType(TextBox) Then
                crt.Text = Nothing
            End If
        Next
    End Sub


    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        Dim mstream As New System.IO.MemoryStream
        PictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
        arrImage = mstream.GetBuffer
        Dim filesize As UInt32
        filesize = mstream.Length

        mstream.Close()
        conn.ConnectionString = Myconnection

        conn.Open()

        sql = "INSERT INTO tblcar(CAR_NAME,PLATE_NO,CAR_BRAND,CAR_SEATS,CAR_CONDITION,RENT_PER_DAY,STATUS,studimg) " & _
       " VALUES (@CARNAME, @PLATENO, @CARBRAND, @CARSEATS, @CARCONDITION, @RENTPERDAY, @STATUS, @studimg)"

        cmd.Connection = conn
        cmd.CommandText = sql
        cmd.Parameters.AddWithValue("@FNAME", Txtcarname.Text)
        cmd.Parameters.AddWithValue("@LNAME", Txtplateno.Text)
        cmd.Parameters.AddWithValue("@COURSE", Txtcarbrand.Text)
        cmd.Parameters.AddWithValue("@YR", Txtcarseats.Text)
        cmd.Parameters.AddWithValue("@AGE", Txtcondition.Text)
        cmd.Parameters.AddWithValue("@RENTPERDAY", Txtrentday.Text)
        cmd.Parameters.AddWithValue("@STATUS", txtavailable.Text)
        cmd.Parameters.AddWithValue("@studimg", arrImage)


        Dim r As Integer
        r = cmd.ExecuteNonQuery()
        If r > 0 Then
            MsgBox("New car has been saved!")
            conn.Close()
            Call Btnload_Click(sender, e)
            cleartextfields()
            PictureBox1.Image = Nothing
        Else
            MsgBox("No car has been saved!")


        End If
        conn.Close()



    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click

        Try
            sql = "Delete * FROM tblcar where CAR_ID=" & Me.Text
            conn.Open()
            With dbcmd
                .CommandText = sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New car has been deleted!")
                conn.Close()
                Call Btnload_Click(sender, e)
                cleartextfields()

            Else
                MsgBox("No car has been deleted!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub Btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnexit.Click
        Me.Close()
    End Sub

    Private Sub Btnupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnupload.Click

        Try
            Dim OFD As FileDialog = New OpenFileDialog()

            OFD.Filter = "Image File (*.jpg;*.bmp;*.gif)| *.jpg;*.bmp;*.gif"

            If OFD.ShowDialog() = DialogResult.OK Then
                imgpath = OFD.FileName
                PictureBox1.ImageLocation = imgpath

            End If

            OFD = Nothing

        Catch ex As Exception
            MsgBox(ex.Message.ToString())

        End Try
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim arrimage() As Byte

        Me.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        Txtpno.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        Txtcname.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        Txtcbrand.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        txtcseats.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString
        Txtcondi.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
        txtrent.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString
        Dim value As Object = DataGridView1.CurrentRow.Cells(7).Value
        arrimage = value
        Cboxstatus.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString
        Dim mstream As New System.IO.MemoryStream(arrimage)
        PictureBox2.Image = Image.FromStream(mstream)

    End Sub


    Private Sub Btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnadd.Click
        Txtcarname.Enabled = True
        Txtplateno.Enabled = True
        Txtcarbrand.Enabled = True
        Txtcarseats.Enabled = True
        Txtcondition.Enabled = True
        Txtrentday.Enabled = True
        txtavailable.Enabled = True
        PictureBox1.Enabled = True
        Btnupload.Enabled = True

    End Sub

    Private Sub Btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnupdate.Click
        Try
            sql = "UPDATE tblcar SET CAR_NAME='" & Txtcname.Text & "', " & _
            " PLATE_NO='" & Txtpno.Text & "', CAR_BRAND='" & Txtcbrand.Text & "', CAR_SEATS='" & txtcseats.Text & "', CAR_CONDITION='" & Txtcondi.Text & "', RENT_PER_DAY='" & txtrent.Text & "', STATUS='" & Cboxstatus.Text & "' WHERE CAR_ID=" & Me.Text

            conn.Open()
            With dbcmd
                .CommandText = sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("Car record has been updated!")
                conn.Close()
                Call Btnload_Click(sender, e)
                cleartextfields()

            Else
                MsgBox("No Car record has been updated!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub Cboxstatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cboxstatus.SelectedIndexChanged
        conn.Open()
        Try
            dbcmd = New OleDbCommand("SELECT * FROM tblcar", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                'Cboxstatus.Items.Add(DbReader("Available").ToString)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()

    End Sub

    Private Sub Txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtsearch.TextChanged

        Try
            conn.ConnectionString = Myconnection

            conn.Open()
            dbdt = New DataTable
            dbcmd = New OleDb.OleDbCommand("Select * from tblcar where CAR_NAME like '%" & Txtsearch.Text & "%'", conn)

            dbda.SelectCommand = dbcmd
            dbda.Fill(dbdt)

            DataGridView1.DataSource = dbdt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        dbda.Dispose()

        conn.Close()
    End Sub

    Private Sub Cars_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AutoFill("SELECT CAR_NAME from tblcar", Txtsearch)
    End Sub
End Class