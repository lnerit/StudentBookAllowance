Public Class frmMainQueryScreen
    Dim q As New dbQuery
    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub frmMainQueryScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Load the year codes to the comboBox
            Dim year As String = "SELECT DISTINCT YearOfSponsor FROM SponsorAllowance ORDER BY YearOfSponsor DESC"
            q.LoadRecordsToControl(year, cmbYear)
            If (cmbYear.Items.Count > 0) Then
                cmbYear.Text = cmbYear.Items(0).ToString()
            End If
            'Load records on form_load event
            If (cmbYear.Text <> "") Then
                Dim str As String = "SELECT SponsorAllowance.StudentId as [StudentId],Student.FirstName,Student.LastName,SponsorAllowance.Sponsor as [Sponsor],SponsorAllowance.YearOfSponsor as [Year]
                                    ,(SponsorAllowance.Amount-(SELECT  Sum(Amount) FROM [Transaction] WHERE StudentId=SponsorAllowance.StudentId AND Sponsor=SponsorAllowance.Sponsor)) as [Available Balance]
                                    FROM SponsorAllowance
                                    INNER JOIN Student ON SponsorAllowance.StudentId=Student.StudentId
                                     WHERE SponsorAllowance.YearOfSponsor='" + cmbYear.Text + "'"
                q.LoadRecordsToControl(str, DataGridView1)
            End If
        Catch
            MessageBox.Show(Err.Description)
        End Try

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            'Load records on form_load event
            If (cmbYear.Text <> "") Then
                Dim str As String = "SELECT SponsorAllowance.StudentId as [StudentId],Student.FirstName,Student.LastName,SponsorAllowance.Sponsor as [Sponsor],SponsorAllowance.YearOfSponsor as [Year]
                                    ,(SponsorAllowance.Amount-(SELECT  Sum(Amount) FROM [Transaction] WHERE StudentId=SponsorAllowance.StudentId AND Sponsor=SponsorAllowance.Sponsor)) as [Available Balance]
                                    FROM SponsorAllowance
                                    INNER JOIN Student ON SponsorAllowance.StudentId=Student.StudentId
                                     WHERE SponsorAllowance.YearOfSponsor='" + cmbYear.Text + "'
                                      AND (SponsorAllowance.StudentId LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%' OR  Student.FirstName LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%' OR Student.LastName LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%' OR SponsorAllowance.Sponsor LIKE '%" + ScanStringForQuote(txtSearch.Text) + "%')"

                q.LoadRecordsToControl(str, DataGridView1)
                If (DataGridView1.RowCount > 0) Then
                    Dim ex As DataGridViewCellEventArgs
                    '  ex = Nullable
                    DataGridView1.Rows(0).Selected = True
                    DataGridView1_CellClick(sender, ex)
                Else
                    ToolStripStatusLabel1.Text = ""
                End If
            Else
                MessageBox.Show("Please select a year first...", "Year value missing...", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
        Catch

            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End Try
    End Sub

    Private Sub cmbYear_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbYear.KeyPress
        e.Handled = True
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If (DataGridView1.RowCount > 0) Then
                Dim row As DataGridViewRow
                For Each row In DataGridView1.Rows
                    If row.Selected Then
                        ToolStripStatusLabel1.Text = "Student Record: " + CStr(row.Cells("FirstName").Value.ToString()) + " " + CStr(row.Cells("LastName").Value.ToString()) + "[" + row.Cells("StudentId").Value.ToString() + "] |==> Available Balance: K" + CStr(row.Cells("Available Balance").Value.ToString())
                    Else
                        ToolStripStatusLabel1.Text = ""
                    End If
                    Exit Sub
                Next
            End If
        Catch
            MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        frmMainQueryScreen_Load(sender, e)
    End Sub
End Class