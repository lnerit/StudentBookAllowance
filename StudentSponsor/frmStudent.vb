Public Class frmStudent
    Dim q As New dbQuery
    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub frmStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ButtonControl("", cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        EnableDisableControlsOnForm(GroupBox1, 0)
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        ButtonControl(cmdAdd.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        EnableDisableControlsOnForm(GroupBox1, 1)
        EnableDisableControlsOnForm(GroupBox1, 2)
        cmdSave.Text = "Save"
        'Load Department codes
        Dim str As String = "SELECT DISTINCT DptCode FROM Departments"
        cmbDepartmentCode.Items.Clear()
        q.LoadRecordsToControl(str, cmbDepartmentCode)
    End Sub

    Private Sub cmdModify_Click(sender As Object, e As EventArgs) Handles cmdModify.Click
        cmdSave.Text = "Update"
        ButtonControl(cmdModify.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        EnableDisableControlsOnForm(GroupBox1, 1)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            If txtStudentId.Text = "" Then
                MessageBox.Show("Please studentId...", "Missing Student ID", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If txtFirstName.Text = "" Then
                MessageBox.Show("Please enter first name...", "Missing First Name", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If txtLastName.Text = "" Then
                MessageBox.Show("Please enter last name..", "Missing Last Name", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbDepartmentCode.Text = "" Then
                MessageBox.Show("Please enter department code...", "Missing Department Code", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If cmbProgramCode.Text = "" Then
                MessageBox.Show("Please program code...", "Missing Program Code", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to save the record?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                If cmdSave.Text = "Save" Then
                    Dim i As String = "INSERT INTO Student VALUES('" + txtStudentId.Text + "','" + ScanStringForQuote(txtFirstName.Text) + "','" + ScanStringForQuote(txtLastName.Text) + "','" + cmbDepartmentCode.Text + "','" + cmbProgramCode.Text + "')"
                    q.ExcecuteNonQuery(i)
                    ButtonControl(cmdSave.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
                    EnableDisableControlsOnForm(GroupBox1, 0)
                End If
                If (cmdSave.Text = "Update") Then
                    Dim u As String = "UPDATE Student SET FirstName='" + ScanStringForQuote(txtFirstName.Text) + "',LastName='" + ScanStringForQuote(txtLastName.Text) + "',DepartmentCode='" + cmbDepartmentCode.Text + "',ProgramCode='" + cmbProgramCode.Text + "' WHERE StudentId='" + txtStudentId.Text + "'"
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
            If txtStudentId.Text = "" Then
                MessageBox.Show("Please studentId...", "Missing Student ID", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to delete the record from the system?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                Dim s As String = "DELETE FROM Student WHERE StudentId='" + txtStudentId.Text + "'"
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

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click

        miscModule.formName = Me.Name
        Dim f As New frmSearch
        miscModule.LoadForm(frmSearch, Application.OpenForms("mdiMain"), "Search")
        If (txtStudentId.Text <> "") Then
            ButtonControl(cmdFind.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        End If
    End Sub
    Public Sub PopulateFieldsOnForm(ByVal dv As DataGridView)
        Dim row As DataGridViewRow
        If dv.RowCount > 0 Then
            For Each row In dv.Rows
                If (row.Selected) Then
                    txtStudentId.Text = row.Cells("StudentId").Value
                    txtFirstName.Text = row.Cells("FirstName").Value
                    txtLastName.Text = row.Cells("LastName").Value
                    cmbDepartmentCode.Text = row.Cells("DepartmentCode").Value
                    cmbProgramCode.Text = row.Cells("ProgramCode").Value
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub cmbDepartmentCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartmentCode.SelectedIndexChanged
        Dim str As String = "SELECT DISTINCT ProgramCode FROM Departments WHERE DptCode='" + cmbDepartmentCode.Text + "'"
        cmbProgramCode.Items.Clear()
        q.LoadRecordsToControl(str, cmbProgramCode)
    End Sub

    Private Sub cmbDepartmentCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDepartmentCode.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbProgramCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbProgramCode.KeyPress
        e.Handled = True
    End Sub
End Class