Imports System.ComponentModel
Imports System.Globalization

Public Class frmSettings


    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Button2.Visible = False
        Button3.Visible = False
        Button4.Visible = False
        Button5.Visible = False
        Button6.Visible = False
        Button7.Visible = False

        txtBoxTemplates.Visible = False
        txtBoxCarts.Visible = False
        txtBoxJobs.Visible = False
        txtBoxPackReports.Visible = False
        txtBoxPack.Visible = False
        txtLogReport.Visible = False


        If My.Settings.chkUseSort Then chkUseSort.Checked = True Else chkUseSort.Checked = False
        If My.Settings.chkUsePack Then chkUsePack.Checked = True Else chkUsePack.Checked = False

        If My.Settings.debugSet Then chkDGV.Checked = True Else chkDGV.Checked = False
        If My.Settings.chkUseLogs Then chkUseLogs.Checked = True Else chkUseLogs.Checked = False

        'Set check box for Language selected
        If My.Settings.chkUseEng Then chkEnglish.Checked = True Else chkEnglish.Checked = False
        If My.Settings.chkUseThai Then chkThai.Checked = True Else chkThai.Checked = False

        btnSetSave.Enabled = False

        Label4.Text = SystemInformation.PrimaryMonitorSize.Height
        Label5.Text = SystemInformation.PrimaryMonitorSize.Width

        If My.Settings.chkUseThai Then
            ChangeLanguage("th-TH")
            frmJobEntry.thaiLang = True
        Else
            ChangeLanguage("en")
            frmJobEntry.thaiLang = False
        End If







    End Sub

    Private Sub ChangeLanguage(ByVal lang As String)
        For Each c As Control In Me.Controls
            Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(frmJobEntry))
            resources.ApplyResources(c, c.Name, New CultureInfo(lang))
        Next c
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmActivate.Show()


    End Sub




    Private Sub btnSetSave_click(sender As Object, e As EventArgs) Handles btnSetSave.Click

        My.Settings.chkUseSort = chkUseSort.CheckState
        My.Settings.chkUsePack = chkUsePack.CheckState
        My.Settings.debugSet = chkDGV.CheckState
        My.Settings.chkUseEng = chkEnglish.CheckState
        My.Settings.chkUseThai = chkThai.CheckState
        My.Settings.chkUseLogs = chkUseLogs.CheckState
        My.Settings.Save()
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

    Private Sub chkUseLogs_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseLogs.CheckedChanged
        btnSetSave.Enabled = True
    End Sub


End Class