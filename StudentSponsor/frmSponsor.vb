Public Class frmSponsor
    Dim q As New dbQuery
    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Try
            cmdSave.Text = "Save"
            EnableDisableControlsOnForm(GroupBox1, 1)
            ButtonControl(cmdAdd.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
            Dim s As String = "SELECT StudentId FROM Student"
            q.LoadRecordsToControl(s, cmbStudentId)

            'Generate year codes from the current year to up to 2017
            For i = 0 To 15
                cmbYear.Items.Add(2017 + i)
            Next
        Catch
            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmdModify_Click(sender As Object, e As EventArgs) Handles cmdModify.Click
        cmdSave.Text = "Update"
        EnableDisableControlsOnForm(GroupBox1, 1)
        ButtonControl(cmdModify.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            If cmbStudentId.Text = "" Then
                MessageBox.Show("Please enter Student Id first...", "Missing Student Id", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbSponsor.Text = "" Then
                MessageBox.Show("Please enter Sponsor first...", "Missing Sponsor name", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If txtAmount.Text = "" Then
                MessageBox.Show("Please amount first...", "Missing Anount", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbYear.Text = "" Then
                MessageBox.Show("Please year first...", "Missing Year", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to save the record?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                If cmdSave.Text = "Save" Then
                    Dim i As String = "INSERT INTO SponsorAllowance VALUES('" + cmbStudentId.Text + "','" + ScanStringForQuote(cmbSponsor.Text) + "'," + txtAmount.Text + "," + cmbYear.Text + ")"
                    q.ExcecuteNonQuery(i)
                    ButtonControl(cmdSave.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
                    EnableDisableControlsOnForm(GroupBox1, 0)
                End If
                If (cmdSave.Text = "Update") Then
                    Dim u As String = "UPDATE SponsorAllowance SET Amount='" + txtAmount.Text + "',YearOfSponsor='" + cmbYear.Text + "' WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + ScanStringForQuote(cmbSponsor.Text) + "'"
                    q.ExcecuteNonQuery(u)
                    ButtonControl(cmdSave.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
                    EnableDisableControlsOnForm(GroupBox1, 0)
                End If
            End If
        Catch
            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Try
            If cmbStudentId.Text = "" Then
                MessageBox.Show("Please enter Student Id first...", "Missing Student Id", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbSponsor.Text = "" Then
                MessageBox.Show("Please enter Sponsor first...", "Missing Sponsor name", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to delete the record from the system?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                Dim s As String = "DELETE FROM SponsorAllowance WHERE StudentId='" + cmbStudentId.Text + "' AND Sponsor='" + ScanStringForQuote(cmbSponsor.Text) + "'"
                q.ExcecuteNonQuery(s)
                EnableDisableControlsOnForm(GroupBox1, 0)
                EnableDisableControlsOnForm(GroupBox1, 2)
                ButtonControl(cmdDelete.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
            End If
        Catch
            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        EnableDisableControlsOnForm(GroupBox1, 0)
        EnableDisableControlsOnForm(GroupBox1, 2)
        ButtonControl(cmdCancel.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)

    End Sub

    Private Sub frmSponsor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnableDisableControlsOnForm(GroupBox1, 0)
        ButtonControl("", cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
    End Sub
    Public Sub PopulateFieldsOnForm(ByVal dv As DataGridView)
        If (dv.RowCount > 0) Then
            Dim row As DataGridViewRow
            For Each row In dv.Rows
                If (row.Selected) Then
                    cmbStudentId.Text = row.Cells("StudentId").Value
                    cmbSponsor.Text = row.Cells("Sponsor").Value
                    txtAmount.Text = row.Cells("Amount").Value
                    cmbYear.Text = row.Cells("YearOfSponsor").Value
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        miscModule.formName = Me.Name
        Dim f As New frmSearch
        miscModule.LoadForm(frmSearch, Application.OpenForms("mdiMain"), "Search")
        If (cmbStudentId.Text <> "" And cmbSponsor.Text <> "") Then
            ButtonControl(cmdFind.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        End If
    End Sub

    Private Sub cmbStudentId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStudentId.SelectedIndexChanged
        Dim s As String = "SELECT SponsorName FROM Sponsors"
        q.LoadRecordsToControl(s, cmbSponsor)
    End Sub

    Private Sub cmbYear_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbYear.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbStudentId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbStudentId.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbSponsor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSponsor.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbSponsor_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles cmbSponsor.MouseDoubleClick
        Dim s As String = "SELECT SponsorName FROM Sponsors"
        cmbSponsor.Items.Clear()
        q.LoadRecordsToControl(s, cmbSponsor)
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
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
End Class