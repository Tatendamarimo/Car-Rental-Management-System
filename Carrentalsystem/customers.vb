Public Class customers

    Private Sub customers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Me.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        Txtfullname.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        Txtage.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        Txtaddress.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        Txtcontactno.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString
        Txtdriverlicense.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
    End Sub

    Public Sub cleartextfields()
        For Each crt As Control In GroupBox1.Controls
            If crt.GetType Is GetType(TextBox) Then
                crt.Text = Nothing
            End If
        Next

    End Sub

    Private Sub Btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnadd.Click
        cleartextfields()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnupdate.Click
        Try
            Sql = "UPDATE tblcustomer SET FULL_NAME='" & Txtfullname.Text & "', " & _
            " ADDRESS='" & Txtaddress.Text & "', DRIVER_LICENSE_NO='" & Txtdriverlicense.Text & "', AGE='" & Txtage.Text & "', CONTACT_NO='" & Txtcontactno.Text & "  ' WHERE ID=" & Me.Text
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New customer record has been updated!")
                conn.Close()
                Call Btnload_Click(sender, e)
                cleartextfields()

            Else
                MsgBox("No customer record has been updated!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub Btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btndelete.Click
        Try
            Sql = "DELETE * FROM tblcustomer  WHERE ID=" & Me.Text
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New customer record has been deleted!")
                conn.Close()
                Call Btnload_Click(sender, e)
                cleartextfields()

            Else
                MsgBox("No customer record has been deleted!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            Sql = "INSERT INTO  tblcustomer(FULL_NAME, AGE, ADDRESS, CONTACT_NO, DRIVER_LICENSE_NO) VALUES ('" & Txtfullname.Text & "', '" & Txtage.Text & "','" & Txtaddress.Text & "','" & Txtcontactno.Text & "', '" & Txtdriverlicense.Text & "')"
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New customer record has been saved!")
                conn.Close()
                Call Btnload_Click(sender, e)
                cleartextfields()

            Else
                MsgBox("No customer record has been saved!!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub Txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtsearch.TextChanged
        Try

            conn.Open()
            dbdt = New DataTable

            With dbcmd
                .Connection = conn
                .CommandText = "Select * from tblcustomer where FULL_NAME like '" & Txtsearch.Text & "%'"
            End With

            dbda.SelectCommand = dbcmd
            dbda.Fill(dbdt)

            DataGridView1.DataSource = dbdt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        dbda.Dispose()

        conn.Close()
    End Sub

    Private Sub Txtcontactno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtcontactno.TextChanged
    End Sub

    Private Sub Btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnload.Click
        dbdt = New DataTable
        Try
            Sql = "SELECT * FROM tblcustomer"
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

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class