<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmdrumSearch
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBoxConeBC = New System.Windows.Forms.TextBox()
        Me.btnDrumSearch = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtBoxProdName = New System.Windows.Forms.TextBox()
        Me.txtBoxDoff = New System.Windows.Forms.TextBox()
        Me.txtBoxPackDate = New System.Windows.Forms.TextBox()
        Me.txtBoxPacker = New System.Windows.Forms.TextBox()
        Me.txtTraceNum = New System.Windows.Forms.TextBox()
        Me.txtStepNum = New System.Windows.Forms.TextBox()
        Me.txtIdxNum = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBoxMCNum = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMergeNum = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(280, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(271, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search for Drum Information"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "DRUM Barcode #"
        '
        'txtBoxConeBC
        '
        Me.txtBoxConeBC.Location = New System.Drawing.Point(181, 68)
        Me.txtBoxConeBC.Name = "txtBoxConeBC"
        Me.txtBoxConeBC.Size = New System.Drawing.Size(212, 26)
        Me.txtBoxConeBC.TabIndex = 4
        '
        'btnDrumSearch
        '
        Me.btnDrumSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDrumSearch.Location = New System.Drawing.Point(399, 66)
        Me.btnDrumSearch.Name = "btnDrumSearch"
        Me.btnDrumSearch.Size = New System.Drawing.Size(96, 30)
        Me.btnDrumSearch.TabIndex = 8
        Me.btnDrumSearch.Text = "Search"
        Me.btnDrumSearch.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(40, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 20)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Product"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(489, 243)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 20)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "TRACE #"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(39, 385)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 20)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Date Packed"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(40, 349)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 20)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Packer"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(40, 279)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 20)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Doff #"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(489, 282)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 20)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Step #"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(489, 318)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 20)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Position #"
        '
        'txtBoxProdName
        '
        Me.txtBoxProdName.Enabled = False
        Me.txtBoxProdName.Location = New System.Drawing.Point(178, 200)
        Me.txtBoxProdName.Name = "txtBoxProdName"
        Me.txtBoxProdName.Size = New System.Drawing.Size(363, 26)
        Me.txtBoxProdName.TabIndex = 19
        '
        'txtBoxDoff
        '
        Me.txtBoxDoff.Enabled = False
        Me.txtBoxDoff.Location = New System.Drawing.Point(178, 276)
        Me.txtBoxDoff.Name = "txtBoxDoff"
        Me.txtBoxDoff.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxDoff.TabIndex = 20
        '
        'txtBoxPackDate
        '
        Me.txtBoxPackDate.Enabled = False
        Me.txtBoxPackDate.Location = New System.Drawing.Point(178, 379)
        Me.txtBoxPackDate.Name = "txtBoxPackDate"
        Me.txtBoxPackDate.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxPackDate.TabIndex = 21
        '
        'txtBoxPacker
        '
        Me.txtBoxPacker.Enabled = False
        Me.txtBoxPacker.Location = New System.Drawing.Point(178, 346)
        Me.txtBoxPacker.Name = "txtBoxPacker"
        Me.txtBoxPacker.Size = New System.Drawing.Size(167, 26)
        Me.txtBoxPacker.TabIndex = 22
        '
        'txtTraceNum
        '
        Me.txtTraceNum.Enabled = False
        Me.txtTraceNum.Location = New System.Drawing.Point(592, 239)
        Me.txtTraceNum.Name = "txtTraceNum"
        Me.txtTraceNum.Size = New System.Drawing.Size(140, 26)
        Me.txtTraceNum.TabIndex = 24
        '
        'txtStepNum
        '
        Me.txtStepNum.Enabled = False
        Me.txtStepNum.Location = New System.Drawing.Point(592, 279)
        Me.txtStepNum.Name = "txtStepNum"
        Me.txtStepNum.Size = New System.Drawing.Size(57, 26)
        Me.txtStepNum.TabIndex = 25
        '
        'txtIdxNum
        '
        Me.txtIdxNum.Enabled = False
        Me.txtIdxNum.Location = New System.Drawing.Point(592, 312)
        Me.txtIdxNum.Name = "txtIdxNum"
        Me.txtIdxNum.Size = New System.Drawing.Size(57, 26)
        Me.txtIdxNum.TabIndex = 26
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(233, 424)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(308, 144)
        Me.DataGridView1.TabIndex = 27
        Me.DataGridView1.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Salmon
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(12, 424)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(118, 47)
        Me.btnCancel.TabIndex = 28
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(39, 315)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(91, 20)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Machine #"
        '
        'txtBoxMCNum
        '
        Me.txtBoxMCNum.Enabled = False
        Me.txtBoxMCNum.Location = New System.Drawing.Point(178, 312)
        Me.txtBoxMCNum.Name = "txtBoxMCNum"
        Me.txtBoxMCNum.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxMCNum.TabIndex = 30
        '
        'txtWeight
        '
        Me.txtWeight.Enabled = False
        Me.txtWeight.Location = New System.Drawing.Point(592, 349)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(57, 26)
        Me.txtWeight.TabIndex = 32
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(489, 352)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 20)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Weight"
        '
        'txtMergeNum
        '
        Me.txtMergeNum.Enabled = False
        Me.txtMergeNum.Location = New System.Drawing.Point(178, 234)
        Me.txtMergeNum.Name = "txtMergeNum"
        Me.txtMergeNum.Size = New System.Drawing.Size(100, 26)
        Me.txtMergeNum.TabIndex = 34
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(40, 242)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 20)
        Me.Label10.TabIndex = 33
        Me.Label10.Text = "Merge #"
        '
        'frmdrumSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 489)
        Me.Controls.Add(Me.txtMergeNum)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtBoxMCNum)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtIdxNum)
        Me.Controls.Add(Me.txtStepNum)
        Me.Controls.Add(Me.txtTraceNum)
        Me.Controls.Add(Me.txtBoxPacker)
        Me.Controls.Add(Me.txtBoxPackDate)
        Me.Controls.Add(Me.txtBoxDoff)
        Me.Controls.Add(Me.txtBoxProdName)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnDrumSearch)
        Me.Controls.Add(Me.txtBoxConeBC)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmdrumSearch"
        Me.Text = "Drum Search"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtBoxConeBC As TextBox
    Friend WithEvents btnDrumSearch As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtBoxProdName As TextBox
    Friend WithEvents txtBoxDoff As TextBox
    Friend WithEvents txtBoxPackDate As TextBox
    Friend WithEvents txtBoxPacker As TextBox
    Friend WithEvents txtTraceNum As TextBox
    Friend WithEvents txtStepNum As TextBox
    Friend WithEvents txtIdxNum As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents txtBoxMCNum As TextBox
    Friend WithEvents txtWeight As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtMergeNum As TextBox
    Friend WithEvents Label10 As Label
End Class
