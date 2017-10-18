Public Class mdiMain

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub LogOffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOffToolStripMenuItem.Click
        Try
            Dim f As New frmLogin
            f = Application.OpenForms("frmLogin")
            f.Show()
            Me.Close()
        Catch
            MessageBox.Show(Err.Description, "", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub StudentRegistryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StudentRegistryToolStripMenuItem.Click

        miscModule.LoadForm(frmStudent, Me, "Student Detail Registry")
    End Sub

    Private Sub SponsorDetailsRegistryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SponsorDetailsRegistryToolStripMenuItem.Click

        miscModule.LoadForm(frmSponsor, Me, "Sponsor Allowance")
    End Sub

    Private Sub AccoutBalanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AccoutBalanceToolStripMenuItem.Click

        miscModule.LoadForm(TransactionDetails, Me, "Purchase Transactions")
    End Sub

    Private Sub mdiMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        miscModule.LoadForm(frmMainQueryScreen, Me, "Allowance Query Screen")
        ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text + " " + UserName
        ToolStripStatusLabel2.Text = DateTime.Now.ToString
        ToolStripStatusLabel3.Text = "WELCOME TO STUDENT BOOK ALLOWANCE TRACKING SYSTEM..."
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        miscModule.LoadForm(AboutBox1, Me, "About Student Allowance System")
    End Sub

    Private Sub NewUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewUserToolStripMenuItem.Click
        miscModule.LoadForm(frmNewUser, Me, "System USer Registry")
    End Sub

    Private Sub StudentListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StudentListToolStripMenuItem.Click
        MessageBox.Show("Not Implemented...", "Student List", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub PurchaseHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseHistoryToolStripMenuItem.Click
        MessageBox.Show("Not Implemented...", "Purchase History", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub SponsorHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SponsorHistoryToolStripMenuItem.Click
        MessageBox.Show("Not Implemented...", "Sponsor History", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub QueryFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueryFormToolStripMenuItem.Click
        miscModule.LoadForm(frmMainQueryScreen, Me, "Student Allowance Query Form")
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CascadeToolStripMenuItem.Click

    End Sub
End Class