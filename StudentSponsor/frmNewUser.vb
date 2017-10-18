Public Class frmNewUser
    Dim q As New dbQuery
    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        ButtonControl(cmdAdd.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        EnableDisableControlsOnForm(GroupBox1, 1)
        EnableDisableControlsOnForm(GroupBox1, 2)
        cmdSave.Text = "Save"
    End Sub

    Private Sub cmdModify_Click(sender As Object, e As EventArgs) Handles cmdModify.Click
        cmdSave.Text = "Update"
        ButtonControl(cmdModify.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        EnableDisableControlsOnForm(GroupBox1, 1)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to save the record?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                If cmdSave.Text = "Save" Then
                    Dim i As String = "INSERT INTO [Login] VALUES('" + txtUserId.Text + "','" + txtPassword.Text + "')"
                    q.ExcecuteNonQuery(i)
                    ButtonControl(cmdSave.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
                    EnableDisableControlsOnForm(GroupBox1, 0)
                End If
                If (cmdSave.Text = "Update") Then
                    Dim u As String = "UPDATE [Login] SET [Password]='" + txtPassword.Text + "' WHERE LoginId='" + txtUserId.Text + "'"
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
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to delete the record from the system?", "Student Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (response = vbYes) Then
                Dim s As String = "DELETE FROM Login WHERE LoginId='" + txtUserId.Text + "'"
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
        If (txtUserId.Text <> "") Then
            ButtonControl(cmdFind.Name, cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        End If
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub frmNewUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ButtonControl("", cmdAdd, cmdModify, cmdSave, cmdDelete, cmdCancel, cmdFind)
        EnableDisableControlsOnForm(GroupBox1, 0)
    End Sub

    Public Sub PopulateFieldsOnForm(ByVal dv As DataGridView)
        Dim row As DataGridViewRow
        If dv.RowCount > 0 Then
            For Each row In dv.Rows
                If (row.Selected) Then
                    txtUserId.Text = row.Cells("LoginId").Value
                    'Get password for the selected user
                    Dim s As String = "SELECT Password FROM Login WHERE LoginId='" + txtUserId.Text + "'"
                    txtPassword.Text = q.ExcecuteScalarQuery(s)
                    Exit Sub
                End If
            Next
        End If
    End Sub
End Class