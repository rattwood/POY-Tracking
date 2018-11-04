<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.btnCancelReport = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.comBoxDrumPal = New System.Windows.Forms.ComboBox()
        Me.ToraydbBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Toraydb = New POY_Tracking.Toraydb()
        Me.lblAutoCorrect = New System.Windows.Forms.Label()
        Me.btnNewPallet = New System.Windows.Forms.Button()
        Me.btnOldPallet = New System.Windows.Forms.Button()
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'btnSettings
        '
        Me.btnSettings.Image = Global.POY_Tracking.My.Resources.Resources.Settings_12x_16x
        resources.ApplyResources(Me.btnSettings, "btnSettings")
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.UseVisualStyleBackColor = True
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
        Me.btnNewPallet.BackColor = System.Drawing.Color.LightGray
        resources.ApplyResources(Me.btnNewPallet, "btnNewPallet")
        Me.btnNewPallet.Name = "btnNewPallet"
        Me.btnNewPallet.UseVisualStyleBackColor = False
        '
        'btnOldPallet
        '
        Me.btnOldPallet.BackColor = System.Drawing.Color.LightGray
        resources.ApplyResources(Me.btnOldPallet, "btnOldPallet")
        Me.btnOldPallet.Name = "btnOldPallet"
        Me.btnOldPallet.UseVisualStyleBackColor = False
        '
        'frmJobEntry
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnOldPallet)
        Me.Controls.Add(Me.btnNewPallet)
        Me.Controls.Add(Me.lblAutoCorrect)
        Me.Controls.Add(Me.comBoxDrumPal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancelReport)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.txtDrumNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtOperator)
        Me.Name = "frmJobEntry"
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtOperator As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDrumNum As TextBox
    Friend WithEvents ToraydbBindingSource As BindingSource
    Friend WithEvents Toraydb As Toraydb
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnCancelReport As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents comBoxDrumPal As ComboBox
    Friend WithEvents lblAutoCorrect As Label
    Friend WithEvents btnNewPallet As Button
    Friend WithEvents btnOldPallet As Button
End Class
