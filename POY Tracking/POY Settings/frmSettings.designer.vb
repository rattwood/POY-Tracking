<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.lblSerialPortSelect = New System.Windows.Forms.Label()
        Me.lstSerialPorts = New System.Windows.Forms.ListBox()
        Me.btnSetSave = New System.Windows.Forms.Button()
        Me.lstBaudRates = New System.Windows.Forms.ListBox()
        Me.chkUseColour = New System.Windows.Forms.CheckBox()
        Me.chkUseSort = New System.Windows.Forms.CheckBox()
        Me.chkUsePack = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxTemplates = New System.Windows.Forms.TextBox()
        Me.txtBoxCarts = New System.Windows.Forms.TextBox()
        Me.txtBoxJobs = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkDGV = New System.Windows.Forms.CheckBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.txtBoxPack = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtBoxPackReports = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBoxBarcodectrl = New System.Windows.Forms.TextBox()
        Me.chkThai = New System.Windows.Forms.CheckBox()
        Me.chkEnglish = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblSerialPortSelect
        '
        resources.ApplyResources(Me.lblSerialPortSelect, "lblSerialPortSelect")
        Me.lblSerialPortSelect.Name = "lblSerialPortSelect"
        '
        'lstSerialPorts
        '
        resources.ApplyResources(Me.lstSerialPorts, "lstSerialPorts")
        Me.lstSerialPorts.FormattingEnabled = True
        Me.lstSerialPorts.Name = "lstSerialPorts"
        '
        'btnSetSave
        '
        resources.ApplyResources(Me.btnSetSave, "btnSetSave")
        Me.btnSetSave.Name = "btnSetSave"
        Me.btnSetSave.UseVisualStyleBackColor = True
        '
        'lstBaudRates
        '
        resources.ApplyResources(Me.lstBaudRates, "lstBaudRates")
        Me.lstBaudRates.FormattingEnabled = True
        Me.lstBaudRates.Items.AddRange(New Object() {resources.GetString("lstBaudRates.Items"), resources.GetString("lstBaudRates.Items1"), resources.GetString("lstBaudRates.Items2"), resources.GetString("lstBaudRates.Items3"), resources.GetString("lstBaudRates.Items4")})
        Me.lstBaudRates.Name = "lstBaudRates"
        '
        'chkUseColour
        '
        resources.ApplyResources(Me.chkUseColour, "chkUseColour")
        Me.chkUseColour.Name = "chkUseColour"
        Me.chkUseColour.UseVisualStyleBackColor = True
        '
        'chkUseSort
        '
        resources.ApplyResources(Me.chkUseSort, "chkUseSort")
        Me.chkUseSort.Name = "chkUseSort"
        Me.chkUseSort.UseVisualStyleBackColor = True
        '
        'chkUsePack
        '
        resources.ApplyResources(Me.chkUsePack, "chkUsePack")
        Me.chkUsePack.Name = "chkUsePack"
        Me.chkUsePack.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'txtBoxTemplates
        '
        resources.ApplyResources(Me.txtBoxTemplates, "txtBoxTemplates")
        Me.txtBoxTemplates.Name = "txtBoxTemplates"
        '
        'txtBoxCarts
        '
        resources.ApplyResources(Me.txtBoxCarts, "txtBoxCarts")
        Me.txtBoxCarts.Name = "txtBoxCarts"
        '
        'txtBoxJobs
        '
        resources.ApplyResources(Me.txtBoxJobs, "txtBoxJobs")
        Me.txtBoxJobs.Name = "txtBoxJobs"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        resources.ApplyResources(Me.Button3, "Button3")
        Me.Button3.Name = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        resources.ApplyResources(Me.Button4, "Button4")
        Me.Button4.Name = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'FolderBrowserDialog1
        '
        resources.ApplyResources(Me.FolderBrowserDialog1, "FolderBrowserDialog1")
        '
        'chkDGV
        '
        resources.ApplyResources(Me.chkDGV, "chkDGV")
        Me.chkDGV.Name = "chkDGV"
        Me.chkDGV.UseVisualStyleBackColor = True
        '
        'Button5
        '
        resources.ApplyResources(Me.Button5, "Button5")
        Me.Button5.Name = "Button5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'txtBoxPack
        '
        resources.ApplyResources(Me.txtBoxPack, "txtBoxPack")
        Me.txtBoxPack.Name = "txtBoxPack"
        '
        'Button6
        '
        resources.ApplyResources(Me.Button6, "Button6")
        Me.Button6.Name = "Button6"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txtBoxPackReports
        '
        resources.ApplyResources(Me.txtBoxPackReports, "txtBoxPackReports")
        Me.txtBoxPackReports.Name = "txtBoxPackReports"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'txtBoxBarcodectrl
        '
        resources.ApplyResources(Me.txtBoxBarcodectrl, "txtBoxBarcodectrl")
        Me.txtBoxBarcodectrl.Name = "txtBoxBarcodectrl"
        '
        'chkThai
        '
        resources.ApplyResources(Me.chkThai, "chkThai")
        Me.chkThai.Name = "chkThai"
        Me.chkThai.UseVisualStyleBackColor = True
        '
        'chkEnglish
        '
        resources.ApplyResources(Me.chkEnglish, "chkEnglish")
        Me.chkEnglish.Name = "chkEnglish"
        Me.chkEnglish.UseVisualStyleBackColor = True
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'frmSettings
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.chkThai)
        Me.Controls.Add(Me.chkEnglish)
        Me.Controls.Add(Me.txtBoxBarcodectrl)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.txtBoxPackReports)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.txtBoxPack)
        Me.Controls.Add(Me.chkDGV)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBoxJobs)
        Me.Controls.Add(Me.txtBoxCarts)
        Me.Controls.Add(Me.txtBoxTemplates)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkUsePack)
        Me.Controls.Add(Me.chkUseSort)
        Me.Controls.Add(Me.chkUseColour)
        Me.Controls.Add(Me.lstBaudRates)
        Me.Controls.Add(Me.btnSetSave)
        Me.Controls.Add(Me.lstSerialPorts)
        Me.Controls.Add(Me.lblSerialPortSelect)
        Me.Name = "frmSettings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSerialPortSelect As Label
    Friend WithEvents lstSerialPorts As ListBox
    Friend WithEvents btnSetSave As Button
    Friend WithEvents lstBaudRates As ListBox
    Friend WithEvents chkUseColour As CheckBox
    Friend WithEvents chkUseSort As CheckBox
    Friend WithEvents chkUsePack As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtBoxTemplates As TextBox
    Friend WithEvents txtBoxCarts As TextBox
    Friend WithEvents txtBoxJobs As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents chkDGV As CheckBox
    Friend WithEvents Button5 As Button
    Friend WithEvents txtBoxPack As TextBox
    Friend WithEvents Button6 As Button
    Friend WithEvents txtBoxPackReports As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtBoxBarcodectrl As TextBox
    Friend WithEvents chkThai As CheckBox
    Friend WithEvents chkEnglish As CheckBox
    Friend WithEvents Label10 As Label
End Class
