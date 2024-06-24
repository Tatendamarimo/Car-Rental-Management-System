Imports System.Data.OleDb
Public Class Booking
    Private Sub Btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnload.Click

        dbdt = New DataTable
        Try
            Sql = "SELECT rentcar.*, car.status FROM tblrentcar rentcar, tblcar car WHERE rentcar.CAR_NAME = car.CAR_NAME "
            conn.Open()
            With dbcmd
                .CommandText = Sql
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

    Private Sub Btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnclose.Click
        Me.Close()
    End Sub

    Private Sub Booking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Txtprocess.Text = Mainform.Label3.Text
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT * FROM tblcar", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                Cboxcarname.Items.Add(DbReader("CAR_NAME").ToString)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            dbcmd = New OleDbCommand("SELECT * FROM tblcustomer", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                Cboxcustomer.Items.Add(DbReader("FULL_NAME").ToString)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        conn.Close()

    End Sub

    Private Sub Cboxcarname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cboxcarname.SelectedIndexChanged
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT * FROM tblcar WHERE CAR_NAME ='" & Cboxcarname.Text & "'", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                Txtboxplate.Text = DbReader("PLATE_NO")
                Txtrent.Text = DbReader("RENT_PER_DAY")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        conn.Close()
    End Sub

    Private Sub Txtnodays_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtnodays.TextChanged
        Txtotal.Text = Val(Txtnodays.Text) * Val(Txtrent.Text)
    End Sub


    Public Sub cleartextfields()
        For Each crt As Control In GroupBox1.Controls
            If crt.GetType Is GetType(TextBox) Then
                crt.Text = Nothing
            End If
        Next
    End Sub

    Private Sub Btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btndelete.Click
        Try
            Sql = "Delete * FROM tblrentcar where ID=" & Me.Text
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New carrent record has been deleted!")
                conn.Close()
                Call Btnload_Click(sender, e)
                cleartextfields()

            Else
                MsgBox("No carrent record has been deleted!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub Cboxcustomer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cboxcustomer.SelectedIndexChanged
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT * FROM tblcustomer WHERE FULL_NAME='" & Cboxcustomer.Text & "'", conn)
            Dim DbReader As OleDbDataReader
            DbReader = dbcmd.ExecuteReader

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        conn.Close()
    End Sub

    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click

        Try
            Sql = "INSERT INTO  tblrentcar(FULL_NAME,CAR_NAME,PLATE_NO,RENT_DATE,RENT_PER_DAY, NO_DAYS,TOTAL,PROCESS_BY) VALUES ('" & Cboxcustomer.Text & "','" & Cboxcarname.Text & "','" & Txtboxplate.Text & "', '" & DateTimePicker1.Value & "','" & Txtrent.Text & "','" & Txtnodays.Text & "', '" & Txtotal.Text & "','" & Txtprocess.Text & "')"

            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New carrent record has been saved!")
                conn.Close()
                Call Btnload_Click(sender, e)
                cleartextfields()

            Else
                MsgBox("No carrent record has been saved!!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        Try
            Sql = "INSERT INTO tblrentdetails(CAR_NAME,FULL_NAME,RENT_DATE,PROCESS_BY) VALUES ('" & Cboxcarname.Text & "','" & Cboxcustomer.Text & "','" & DateTimePicker1.Value & "','" & Mainform.Label3.Text & "')"
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With
            dbcmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        Btnpayment.Enabled = True
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Me.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        Cbcusname.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        Cbcarname.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        Txtpno.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        DateTimePicker2.Value = DataGridView1.CurrentRow.Cells(4).Value
        Txtrday.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
        Txtndays.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString
        Txttotal.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString
        Txtproces.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString
        Txtstatus.Text = DataGridView1.CurrentRow.Cells(9).Value.ToString
    End Sub


    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Dim days As New Integer

        days = DateTimePicker1.Value.Day - DateTime.Today.Day
        Txtnodays.Text = days.ToString()
        If Val(Txtnodays.Text) < 0 Then
            Txtnodays.Text = 0
        End If

    End Sub

    Private Sub Btnpayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnpayment.Click

        If Txtstatus.Text = "Unavailable" Then
            Btnpayment.Enabled = False
        End If
        payment.Show()
        payment.txttotal.Text = Txttotal.Text

    End Sub

    Private Sub Txtprocess_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtprocess.TextChanged

    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Me.Hide()
        Call Btnload_Click(sender, e)
        Me.Show()
    End Sub
End Class
