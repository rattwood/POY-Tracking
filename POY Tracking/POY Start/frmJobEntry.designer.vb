﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmJobEntry
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJobEntry))
        Me.txtOperator = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDrumNum = New System.Windows.Forms.TextBox()
        Me.btnCancelReport = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.comBoxDrumPal = New System.Windows.Forms.ComboBox()
        Me.ToraydbBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Toraydb = New POY_Tracking.Toraydb()
        Me.lblAutoCorrect = New System.Windows.Forms.Label()
        Me.btnNewPallet = New System.Windows.Forms.Button()
        Me.btnOldPallet = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PalletReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TraceNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DrumNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DISPLAYToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditPalletToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ChangePalletToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.DRUMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.TraceNumberToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.JobControlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReleaseToPackingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtCartNum = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtOperator
        '
        resources.ApplyResources(Me.txtOperator, "txtOperator")
        Me.txtOperator.Name = "txtOperator"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'txtDrumNum
        '
        resources.ApplyResources(Me.txtDrumNum, "txtDrumNum")
        Me.txtDrumNum.Name = "txtDrumNum"
        '
        'btnCancelReport
        '
        Me.btnCancelReport.BackColor = System.Drawing.Color.Red
        resources.ApplyResources(Me.btnCancelReport, "btnCancelReport")
        Me.btnCancelReport.Name = "btnCancelReport"
        Me.btnCancelReport.UseVisualStyleBackColor = False
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.BackColor = System.Drawing.Color.Red
        Me.Label3.Name = "Label3"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'comBoxDrumPal
        '
        resources.ApplyResources(Me.comBoxDrumPal, "comBoxDrumPal")
        Me.comBoxDrumPal.FormattingEnabled = True
        Me.comBoxDrumPal.Items.AddRange(New Object() {resources.GetString("comBoxDrumPal.Items"), resources.GetString("comBoxDrumPal.Items1"), resources.GetString("comBoxDrumPal.Items2")})
        Me.comBoxDrumPal.Name = "comBoxDrumPal"
        '
        'ToraydbBindingSource
        '
        Me.ToraydbBindingSource.DataSource = Me.Toraydb
        Me.ToraydbBindingSource.Position = 0
        '
        'Toraydb
        '
        Me.Toraydb.DataSetName = "Toraydb"
        Me.Toraydb.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblAutoCorrect
        '
        resources.ApplyResources(Me.lblAutoCorrect, "lblAutoCorrect")
        Me.lblAutoCorrect.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblAutoCorrect.Name = "lblAutoCorrect"
        '
        'btnNewPallet
        '
        Me.btnNewPallet.BackColor = System.Drawing.Color.SkyBlue
        resources.ApplyResources(Me.btnNewPallet, "btnNewPallet")
        Me.btnNewPallet.Name = "btnNewPallet"
        Me.btnNewPallet.UseVisualStyleBackColor = False
        '
        'btnOldPallet
        '
        Me.btnOldPallet.BackColor = System.Drawing.Color.SkyBlue
        resources.ApplyResources(Me.btnOldPallet, "btnOldPallet")
        Me.btnOldPallet.Name = "btnOldPallet"
        Me.btnOldPallet.UseVisualStyleBackColor = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.SearchToolStripMenuItem, Me.JobControlToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Image = Global.POY_Tracking.My.Resources.Resources.Settings_12x_16x
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        resources.ApplyResources(Me.SettingsToolStripMenuItem, "SettingsToolStripMenuItem")
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PalletReportToolStripMenuItem, Me.DISPLAYToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Image = Global.POY_Tracking.My.Resources.Resources.Reports
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        resources.ApplyResources(Me.ReportsToolStripMenuItem, "ReportsToolStripMenuItem")
        '
        'PalletReportToolStripMenuItem
        '
        Me.PalletReportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TraceNumberToolStripMenuItem, Me.DrumNumberToolStripMenuItem})
        Me.PalletReportToolStripMenuItem.Name = "PalletReportToolStripMenuItem"
        resources.ApplyResources(Me.PalletReportToolStripMenuItem, "PalletReportToolStripMenuItem")
        '
        'TraceNumberToolStripMenuItem
        '
        Me.TraceNumberToolStripMenuItem.Name = "TraceNumberToolStripMenuItem"
        resources.ApplyResources(Me.TraceNumberToolStripMenuItem, "TraceNumberToolStripMenuItem")
        '
        'DrumNumberToolStripMenuItem
        '
        Me.DrumNumberToolStripMenuItem.Name = "DrumNumberToolStripMenuItem"
        resources.ApplyResources(Me.DrumNumberToolStripMenuItem, "DrumNumberToolStripMenuItem")
        '
        'DISPLAYToolStripMenuItem
        '
        Me.DISPLAYToolStripMenuItem.Name = "DISPLAYToolStripMenuItem"
        resources.ApplyResources(Me.DISPLAYToolStripMenuItem, "DISPLAYToolStripMenuItem")
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.EditPalletToolStripMenuItem, Me.ToolStripSeparator2, Me.ChangePalletToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Image = Global.POY_Tracking.My.Resources.Resources.Tools
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'EditPalletToolStripMenuItem
        '
        Me.EditPalletToolStripMenuItem.Name = "EditPalletToolStripMenuItem"
        resources.ApplyResources(Me.EditPalletToolStripMenuItem, "EditPalletToolStripMenuItem")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'ChangePalletToolStripMenuItem
        '
        Me.ChangePalletToolStripMenuItem.Name = "ChangePalletToolStripMenuItem"
        resources.ApplyResources(Me.ChangePalletToolStripMenuItem, "ChangePalletToolStripMenuItem")
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator3, Me.DRUMToolStripMenuItem, Me.ToolStripSeparator4, Me.TraceNumberToolStripMenuItem1})
        Me.SearchToolStripMenuItem.Image = Global.POY_Tracking.My.Resources.Resources.Search
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        resources.ApplyResources(Me.SearchToolStripMenuItem, "SearchToolStripMenuItem")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'DRUMToolStripMenuItem
        '
        Me.DRUMToolStripMenuItem.Name = "DRUMToolStripMenuItem"
        resources.ApplyResources(Me.DRUMToolStripMenuItem, "DRUMToolStripMenuItem")
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'TraceNumberToolStripMenuItem1
        '
        Me.TraceNumberToolStripMenuItem1.Name = "TraceNumberToolStripMenuItem1"
        resources.ApplyResources(Me.TraceNumberToolStripMenuItem1, "TraceNumberToolStripMenuItem1")
        '
        'JobControlToolStripMenuItem
        '
        Me.JobControlToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReleaseToPackingToolStripMenuItem})
        Me.JobControlToolStripMenuItem.Name = "JobControlToolStripMenuItem"
        resources.ApplyResources(Me.JobControlToolStripMenuItem, "JobControlToolStripMenuItem")
        '
        'ReleaseToPackingToolStripMenuItem
        '
        Me.ReleaseToPackingToolStripMenuItem.Name = "ReleaseToPackingToolStripMenuItem"
        resources.ApplyResources(Me.ReleaseToPackingToolStripMenuItem, "ReleaseToPackingToolStripMenuItem")
        '
        'txtCartNum
        '
        resources.ApplyResources(Me.txtCartNum, "txtCartNum")
        Me.txtCartNum.Name = "txtCartNum"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'frmJobEntry
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtCartNum)
        Me.Controls.Add(Me.btnOldPallet)
        Me.Controls.Add(Me.btnNewPallet)
        Me.Controls.Add(Me.lblAutoCorrect)
        Me.Controls.Add(Me.comBoxDrumPal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancelReport)
        Me.Controls.Add(Me.txtDrumNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtOperator)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmJobEntry"
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtOperator As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDrumNum As TextBox
    Friend WithEvents ToraydbBindingSource As BindingSource
    Friend WithEvents Toraydb As Toraydb
    Friend WithEvents btnCancelReport As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents comBoxDrumPal As ComboBox
    Friend WithEvents lblAutoCorrect As Label
    Friend WithEvents btnNewPallet As Button
    Friend WithEvents btnOldPallet As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PalletReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TraceNumberToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DrumNumberToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditPalletToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DRUMToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TraceNumberToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ChangePalletToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtCartNum As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents DISPLAYToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobControlToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReleaseToPackingToolStripMenuItem As ToolStripMenuItem
End Class
