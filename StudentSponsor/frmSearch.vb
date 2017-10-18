Public Class frmSearch
    Dim student As New frmStudent
    Dim sponsor As New frmSponsor
    Dim transactions As New TransactionDetails
    Dim login As New frmNewUser
    Dim q As New dbQuery
    Private Sub frmSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If (miscModule.formName = student.Name) Then
                Dim s As String = "SELECT * FROM Student"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
            If (miscModule.formName = sponsor.Name) Then
                Dim s As String = "SELECT * FROM SponsorAllowance"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
            If (miscModule.formName = transactions.Name) Then
                Dim s As String = "SELECT * FROM SponsorAllowance"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
            If (miscModule.formName = login.Name) Then
                Dim s As String = "SELECT LoginId FROM Login"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
        Catch
            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Try
            If (Me.DataGridView1.RowCount > 0) Then
                Dim row As DataGridViewRow
                For Each row In Me.DataGridView1.Rows
                    If (row.Selected) Then
                        If (miscModule.formName = student.Name) Then
                            Dim f As New frmStudent
                            f = Application.OpenForms("frmStudent")
                            f.PopulateFieldsOnForm(Me.DataGridView1)
                            Me.Close()
                            Exit Sub
                        End If
                        If (miscModule.formName = sponsor.Name) Then
                            Dim f As New frmSponsor
                            f = Application.OpenForms("frmSponsor")
                            f.PopulateFieldsOnForm(Me.DataGridView1)
                            Me.Close()
                            Exit Sub
                        End If
                        If (miscModule.formName = transactions.Name) Then
                            Dim f As New TransactionDetails
                            f = Application.OpenForms("TransactionDetails")
                            f.PopulateFieldsOnForm(Me.DataGridView1)
                            Me.Close()
                            Exit Sub
                        End If
                        If (miscModule.formName = login.Name) Then
                            Dim f As New frmNewUser
                            f = Application.OpenForms("frmNewUser")
                            f.PopulateFieldsOnForm(Me.DataGridView1)
                            Me.Close()
                            Exit Sub
                        End If
                    End If
                Next
            End If
        Catch
            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If (miscModule.formName = student.Name) Then
                Dim s As String = "SELECT * FROM Student WHERE StudentId LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%' OR FirstName LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%' OR LastName LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%'"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
            If (miscModule.formName = sponsor.Name) Then
                Dim s As String = "SELECT * FROM SponsorAllowance WHERE StudentId='%" + ScanStringForQuote(txtSearch.Text) + "%' OR Sponsor LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%'"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
            If (miscModule.formName = transactions.Name) Then
                Dim s As String = "SELECT * FROM [SponsorAllowance] WHERE StudentId LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%' OR Sponsor LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%' OR YearOfSponsor LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%'"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
            If (miscModule.formName = login.Name) Then
                Dim s As String = "SELECT LoginID FROM Login WHERE LoginId LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%'"
                q.LoadRecordsToControl(s, DataGridView1)
            End If
        Catch
            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        DataGridView1_DoubleClick(sender, e)
    End Sub
End Class