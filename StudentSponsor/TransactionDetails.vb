Imports System.Data.OleDb
Public Class TransactionDetails
    Dim q As New dbQuery
    Dim c As New dbConnection
    Dim aBalance As String
    Private Sub TransactionDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnableDisableControlsOnForm(GroupBox1, 0)
        EnableDisableControlsOnForm(GroupBox3, 0)
        ButtonControl("", cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
    End Sub
    Public Sub PopulateFieldsOnForm(ByVal dv As DataGridView)
        If (dv.RowCount > 0) Then
            Dim row As DataGridViewRow
            Dim e As EventArgs
            'e = ColumnClickEventArgs
            For Each row In dv.Rows
                If (row.Selected) Then
                    cmbStudentId.Text = row.Cells("StudentId").Value
                    cmbSponsor.Text = row.Cells("Sponsor").Value
                    txtAmoutAllocated.Text = row.Cells("Amount").Value.ToString()
                    cmbYear.Text = row.Cells("YearOfSponsor").Value.ToString()
                    cmbSponsor_SelectedIndexChanged(dv, e)

                    If (getBalance(cmbStudentId.Text, cmbSponsor.Text, cmbYear.Text) = "0") Then
                        txtAvailableBalance.Text = txtAmoutAllocated.Text
                    Else
                        txtAvailableBalance.Text = getBalance(cmbStudentId.Text, cmbSponsor.Text, cmbYear.Text)
                    End If
                    aBalance = txtAvailableBalance.Text
                    Exit Sub
                End If
            Next
        End If
    End Sub
    'Get the current balance
    Private Function getBalance(ByVal StudentId As String, ByVal Sponsor As String, ByVal Year As String) As String
        Dim str As String = "SELECT COUNT(*) FROM [Transaction]  INNER JOIN SponsorAllowance ON [Transaction].StudentId=SponsorAllowance.StudentId
                            AND [Transaction].Sponsor=SponsorAllowance.Sponsor
                            WHERE SponsorAllowance.StudentId='" + StudentId + "' AND SponsorAllowance.Sponsor='" + Sponsor + "' AND YearOfSponsor='" + Year + "'"
        If (q.RecordExist(str)) Then
            Dim s As String = "SELECT Amount-(select sum(Amount) from [Transaction] WHERE StudentId='" + StudentId + "' AND Sponsor='" + Sponsor + "') FROM SponsorAllowance  WHERE StudentId='" + StudentId + "' AND Sponsor='" + Sponsor + "' AND YearOfSponsor='" + Year + "'"
            getBalance = q.ExcecuteScalarQuery(s)
        Else
            getBalance = "0"
        End If
    End Function
    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Dim d As DateTime = DateTimePicker1.Text
        mskTransactionDate.Text = d.ToShortDateString
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        cmdSave.Text = "Save"
        EnableDisableControlsOnForm(GroupBox1, 1)
        EnableDisableControlsOnForm(GroupBox1, 2)
        EnableDisableControlsOnForm(GroupBox3, 1)
        EnableDisableControlsOnForm(GroupBox3, 2)
        ButtonControl(cmdAdd.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)

        'Load year to cmbYear combobox
        Dim s As String = "SELECT DISTINCT YearOfSponsor FROM SponsorAllowance ORDER BY YearOfSponsor DESC"
        q.LoadRecordsToControl(s, cmbYear)
    End Sub

    Private Sub cmdModify_Click(sender As Object, e As EventArgs) Handles cmdModify.Click
        cmdSave.Text = "Update"
        EnableDisableControlsOnForm(GroupBox1, 1)
        EnableDisableControlsOnForm(GroupBox3, 1)
        ButtonControl(cmdModify.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            If (txtAvailableBalance.Text <> "") Then
                If (Double.Parse(txtAvailableBalance.Text) < 0) Then
                    MessageBox.Show("The amount you entered exceeded the available balance", "Amount Exceeded Available balance", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    ' Exit Sub
                End If
            End If
            If cmbYear.Text = "" Then
                MessageBox.Show("Please enter Year first...", "Missing Year", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbStudentId.Text = "" Then
                MessageBox.Show("Please enter student Id first...", "Missing StudentId", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbSponsor.Text = "" Then
                MessageBox.Show("Please enter sponsor first...", "Missing Spnsor", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If txtAmountDue.Text = "" Then
                MessageBox.Show("Please enter amount due...", "Missing Due Amount", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If

            Dim response As DialogResult = MessageBox.Show("Are you sure you want to save the record?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                If cmdSave.Text = "Save" Then
                    Dim i As String = "INSERT INTO [Transaction](StudentId,Sponsor,TransactionDate,Amount,Description) VALUES('" + cmbStudentId.Text + "','" + cmbSponsor.Text + "','" + mskTransactionDate.Text + "'," + txtAmountDue.Text + ",'" + txtDescription.Text + "')"
                    q.ExcecuteNonQuery(i)
                    ButtonControl(cmdSave.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
                    EnableDisableControlsOnForm(GroupBox1, 0)
                    EnableDisableControlsOnForm(GroupBox3, 0)
                End If
                If (cmdSave.Text = "Update") Then
                    Dim u As String = "UPDATE [Transaction] SET Amount='" + txtAmountDue.Text + "',TransactionDate='" + mskTransactionDate.Text + "',Description='" + txtDescription.Text + "' WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "' AND Slno=" + txtSlno.Text
                    q.ExcecuteNonQuery(u)
                    ButtonControl(cmdSave.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
                    EnableDisableControlsOnForm(GroupBox1, 0)
                    EnableDisableControlsOnForm(GroupBox3, 0)
                End If
                cmbSponsor_SelectedIndexChanged(sender, e)
            End If
        Catch
            MessageBox.Show(Err.GetException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Try
            If cmbYear.Text = "" Then
                MessageBox.Show("Please enter Year first...", "Missing Year", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbStudentId.Text = "" Then
                MessageBox.Show("Please enter student Id first...", "Missing StudentId", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbSponsor.Text = "" Then
                MessageBox.Show("Please enter sponsor first...", "Missing Spnsor", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If (txtSlno.Text = "") Then
                MessageBox.Show("Please select a record first...", "Record selection", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to delete the record from the system?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                Dim s As String = "DELETE FROM [Transaction] WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "' AND Slno=" + txtSlno.Text
                q.ExcecuteNonQuery(s)
                EnableDisableControlsOnForm(GroupBox1, 0)
                EnableDisableControlsOnForm(GroupBox1, 2)
                EnableDisableControlsOnForm(GroupBox3, 0)
                EnableDisableControlsOnForm(GroupBox3, 2)
                ButtonControl(cmdDelete.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
            End If
        Catch
            MessageBox.Show(Err.GetException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        EnableDisableControlsOnForm(GroupBox1, 0)
        EnableDisableControlsOnForm(GroupBox1, 2)
        EnableDisableControlsOnForm(GroupBox3, 0)
        EnableDisableControlsOnForm(GroupBox3, 2)
        ButtonControl(cmdCancel.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        miscModule.formName = Me.Name
        Dim f As New frmSearch
        miscModule.LoadForm(frmSearch, Application.OpenForms("mdiMain"), "Search")
        ButtonControl(cmdFind.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
    End Sub

    Private Sub cmbStudentId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStudentId.SelectedIndexChanged
        Try
            Dim s As String = "SELECT Sponsor FROM SponsorAllowance WHERE YearOfSponsor='" + cmbYear.Text + "' AND StudentId='" + cmbStudentId.Text + "'"
            q.LoadRecordsToControl(s, cmbSponsor)
        Catch
            MessageBox.Show(Err.GetException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Try
            Dim s As String = "SELECT DISTINCT StudentId FROM SponsorAllowance WHERE YearOfSponsor='" + cmbYear.Text + "'"
            q.LoadRecordsToControl(s, cmbStudentId)
        Catch
            MessageBox.Show(Err.GetException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmbSponsor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSponsor.SelectedIndexChanged
        Try

            'Load the Available balance for the selected student based on sponsor for the selected year.
            'Check first whether there is already a transaction done against the current allowance
            Dim str As String = "SELECT COUNT(*) FROM [Transaction]  INNER JOIN SponsorAllowance ON [Transaction].StudentId=SponsorAllowance.StudentId
                            AND [Transaction].Sponsor=SponsorAllowance.Sponsor
                            WHERE SponsorAllowance.StudentId='" + cmbStudentId.Text + "' AND SponsorAllowance.Sponsor='" + cmbSponsor.Text + "' AND YearOfSponsor='" + cmbYear.Text + "'"
            If (q.RecordExist(str)) Then
                Dim b As String = "SELECT Amount-(SELECT ISNULL(SUM(Amount))=0 FROM [Transaction] 
                            WHERE  StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "') FROM SponsorAllowance 
                            WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "' AND YearOfSponsor='" + cmbYear.Text + "'"
                q.LoadRecordsToControl(b, txtAvailableBalance)

                'Load the transaction history
                Dim s As String = "SELECT Slno,TransactionDate,Amount FROM [Transaction] WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "'"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
            'Dim bal As String = "Select Amount FROM SponsorAllowance WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "' AND YearOfSponsor='" + cmbYear.Text + "'"
            Dim bal As String = "SELECT Amount   FROM SponsorAllowance WHERE SponsorAllowance.StudentId='" + cmbStudentId.Text + "' AND SponsorAllowance.Sponsor='" + cmbSponsor.Text + "' AND YearOfSponsor='" + cmbYear.Text + "'"
            q.LoadRecordsToControl(bal, txtAmoutAllocated)

            'Dim sx As String = "SELECT Amount   FROM [Transaction] WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "'"
            'Dim str As String = q.ExcecuteScalarQuery(b)
            'txtAvailableBalance.Text = str
            If (getBalance(cmbStudentId.Text, cmbSponsor.Text, cmbYear.Text) = "0") Then
                txtAvailableBalance.Text = txtAmoutAllocated.Text
            Else
                txtAvailableBalance.Text = getBalance(cmbStudentId.Text, cmbSponsor.Text, cmbYear.Text)
            End If

            If Not (cmdAdd.Enabled) Then
                If txtAvailableBalance.Text <> "" Then
                    aBalance = txtAvailableBalance.Text
                End If
            Else


            End If
        Catch
            MessageBox.Show(Err.GetException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub txtAmountDue_TextChanged(sender As Object, e As EventArgs) Handles txtAmountDue.TextChanged
        Try
            If Not cmdAdd.Enabled And cmdSave.Text = "Save" Then
                If (txtAvailableBalance.Text <> "" And txtAmountDue.Text <> "") Then
                    Dim a As Double = Double.Parse(txtAmountDue.Text)
                    'Dim b As Decimal = Decimal.TryParse(txtAvailableBalance.Text, vbDecimal)
                    txtAvailableBalance.Text = (Double.Parse(aBalance) - a).ToString()
                    If (Double.Parse(aBalance) - a) < 0 Then
                        MessageBox.Show("The amount you entered exceeded the available balance", "Amount Exceeded Available balance", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End If
                Else
                        txtAvailableBalance.Text = aBalance
                End If
            End If
        Catch
            MessageBox.Show(Err.Description)
        End Try

    End Sub

    Private Sub txtAmountDue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmountDue.KeyPress
        Dim txt As TextBox = CType(sender, TextBox)
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point
        If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number
        'Enter key move to next control
        If e.KeyChar = Chr(13) Then
            GetNextControl(txt, True).Focus()
            'If only a decimal point is in the box clear TextBox
            If e.KeyChar = Chr(13) And txt.Text = "." Then txt.Clear()
            Exit Sub
        End If
        Dim i As Integer = txt.Text.IndexOf(".")
        Dim len As Integer = txt.Text.Length
        'Allow only 2 Decimal places
        If Not e.Handled Then
            If i = -1 Then
                e.Handled = False
            Else
                If (len - i) > 2 Then e.Handled = True
            End If
        End If
        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim conn As New OleDbConnection
        Try

            If (DataGridView1.RowCount > 0) Then
                Dim row As DataGridViewRow
                Dim reader As OleDbDataReader
                conn = c.getConnection()
                For Each row In DataGridView1.Rows
                    If (row.Selected) Then
                        txtSlno.Text = row.Cells("Slno").Value.ToString()
                        Dim ss As String = "SELECT TransactionDate,Amount,Description FROM [Transaction] WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + cmbSponsor.Text + "' AND Slno=" + row.Cells("Slno").Value.ToString()
                        Dim cmd As OleDbCommand = New OleDbCommand(ss, conn)
                        reader = cmd.ExecuteReader()
                        While (reader.Read())
                            mskTransactionDate.Text = reader.GetValue(0).ToString()
                            txtAmountDue.Text = reader.GetValue(1).ToString()
                            txtDescription.Text = reader.GetValue(2).ToString()
                        End While
                        conn.Close()
                        Exit Sub
                    End If
                Next

            End If
        Catch
            conn.Close()
            MessageBox.Show(Err.Description)
        End Try

    End Sub

    Private Sub cmdPreview_Click(sender As Object, e As EventArgs) Handles cmdPreview.Click
        MessageBox.Show("Report Printing Not yet implemented..")
    End Sub
End Class