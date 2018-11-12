Public Class frmSettings
    Public PortCom
    Public PortBaudRate
    Public softwareActivation

    Dim myPort As Array



    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'Shows current settings
        'txtBoxTemplates.Text = My.Settings.dirTemplate
        'txtBoxCarts.Text = My.Settings.dirCarts
        'txtBoxJobs.Text = My.Settings.dirJobs
        'txtBoxPack.Text = My.Settings.dirPacking
        'txtBoxPackReports.Text = My.Settings.dirPackReports




        If My.Settings.chkUseSort Then chkUseSort.Checked = True Else chkUseSort.Checked = False
        If My.Settings.chkUsePack Then chkUsePack.Checked = True Else chkUsePack.Checked = False

        If My.Settings.debugSet Then chkDGV.Checked = True Else chkDGV.Checked = False

        'Set check box for Language selected
        If My.Settings.chkUseEng Then chkEnglish.Checked = True Else chkEnglish.Checked = False
        If My.Settings.chkUseThai Then chkThai.Checked = True Else chkThai.Checked = False

        btnSetSave.Enabled = False

        Label4.Text = SystemInformation.PrimaryMonitorSize.Height
        Label5.Text = SystemInformation.PrimaryMonitorSize.Width



    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmActivate.Show()


    End Sub



    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    '    FolderBrowserDialog1.ShowDialog()
    '    txtBoxTemplates.Text = FolderBrowserDialog1.SelectedPath
    '    btnSetSave.Enabled = True

    'End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    '    FolderBrowserDialog1.ShowDialog()
    '    txtBoxCarts.Text = FolderBrowserDialog1.SelectedPath
    '    btnSetSave.Enabled = True
    'End Sub

    'Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    '    FolderBrowserDialog1.ShowDialog()
    '    txtBoxJobs.Text = FolderBrowserDialog1.SelectedPath
    '    btnSetSave.Enabled = True
    'End Sub

    'Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
    '    FolderBrowserDialog1.ShowDialog()
    '    txtBoxPack.Text = FolderBrowserDialog1.SelectedPath
    '    btnSetSave.Enabled = True
    'End Sub

    'Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    FolderBrowserDialog1.ShowDialog()
    '    txtBoxPackReports.Text = FolderBrowserDialog1.SelectedPath
    '    btnSetSave.Enabled = True
    'End Sub

    Private Sub btnSetSave_click(sender As Object, e As EventArgs) Handles btnSetSave.Click

        My.Settings.chkUseSort = chkUseSort.CheckState
        My.Settings.chkUsePack = chkUsePack.CheckState
        My.Settings.debugSet = chkDGV.CheckState
        My.Settings.chkUseEng = chkEnglish.CheckState
        My.Settings.chkUseThai = chkThai.CheckState
        'My.Settings.dirTemplate = txtBoxTemplates.Text
        'My.Settings.dirCarts = txtBoxCarts.Text
        'My.Settings.dirJobs = txtBoxJobs.Text
        'My.Settings.dirPacking = txtBoxPack.Text
        'My.Settings.dirPackReports = txtBoxPackReports.Text

        Me.Close()
    End Sub


    Private Sub chkUseSort_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseSort.CheckedChanged
        chkUsePack.CheckState = False
        chkUsePack.Checked = False
        btnSetSave.Enabled = True
    End Sub

    Private Sub chkUsePack_CheckedChanged(sender As Object, e As EventArgs) Handles chkUsePack.CheckedChanged
        chkUseSort.CheckState = False
        chkUseSort.Checked = False
        btnSetSave.Enabled = True
    End Sub

    Private Sub chkEnglish_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnglish.CheckedChanged
        chkThai.CheckState = False
        chkThai.Checked = False
        btnSetSave.Enabled = True
    End Sub

    Private Sub chkThai_CheckedChanged(sender As Object, e As EventArgs) Handles chkThai.CheckedChanged
        chkEnglish.CheckState = False
        chkEnglish.Checked = False
        btnSetSave.Enabled = True
    End Sub
End Class