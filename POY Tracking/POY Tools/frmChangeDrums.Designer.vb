<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeDrums
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGVChageDrum = New System.Windows.Forms.DataGridView()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnCheck = New System.Windows.Forms.Button()
        Me.POYPACKIDX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POYSTEPNUM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POYBCODEDRUM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POYREPBCODEDRUM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POYPACKDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGVChageDrum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVChageDrum
        '
        Me.DGVChageDrum.AllowUserToAddRows = False
        Me.DGVChageDrum.AllowUserToDeleteRows = False
        Me.DGVChageDrum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGVChageDrum.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DGVChageDrum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVChageDrum.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.POYPACKIDX, Me.POYSTEPNUM, Me.POYBCODEDRUM, Me.POYREPBCODEDRUM, Me.POYPACKDATE})
        Me.DGVChageDrum.Location = New System.Drawing.Point(12, 24)
        Me.DGVChageDrum.Name = "DGVChageDrum"
        Me.DGVChageDrum.Size = New System.Drawing.Size(438, 525)
        Me.DGVChageDrum.TabIndex = 0
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.LightGreen
        Me.btnUpdate.Enabled = False
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(505, 470)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(180, 71)
        Me.btnUpdate.TabIndex = 342
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.Gold
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(505, 24)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(180, 71)
        Me.btnClear.TabIndex = 341
        Me.btnClear.Text = "Clear Changes"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(505, 105)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(180, 71)
        Me.btnCancel.TabIndex = 340
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnCheck
        '
        Me.btnCheck.BackColor = System.Drawing.Color.White
        Me.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheck.Location = New System.Drawing.Point(505, 393)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(180, 71)
        Me.btnCheck.TabIndex = 343
        Me.btnCheck.Text = "Check Entries"
        Me.btnCheck.UseVisualStyleBackColor = False
        '
        'POYPACKIDX
        '
        Me.POYPACKIDX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.POYPACKIDX.DefaultCellStyle = DataGridViewCellStyle1
        Me.POYPACKIDX.HeaderText = "Index"
        Me.POYPACKIDX.Name = "POYPACKIDX"
        Me.POYPACKIDX.ReadOnly = True
        Me.POYPACKIDX.Width = 58
        '
        'POYSTEPNUM
        '
        Me.POYSTEPNUM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.POYSTEPNUM.DefaultCellStyle = DataGridViewCellStyle2
        Me.POYSTEPNUM.HeaderText = "Step No."
        Me.POYSTEPNUM.Name = "POYSTEPNUM"
        Me.POYSTEPNUM.ReadOnly = True
        Me.POYSTEPNUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.POYSTEPNUM.Width = 74
        '
        'POYBCODEDRUM
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.POYBCODEDRUM.DefaultCellStyle = DataGridViewCellStyle3
        Me.POYBCODEDRUM.HeaderText = "DRUM No."
        Me.POYBCODEDRUM.Name = "POYBCODEDRUM"
        Me.POYBCODEDRUM.ReadOnly = True
        Me.POYBCODEDRUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.POYBCODEDRUM.Width = 120
        '
        'POYREPBCODEDRUM
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.POYREPBCODEDRUM.DefaultCellStyle = DataGridViewCellStyle4
        Me.POYREPBCODEDRUM.HeaderText = "Replacment Drum No."
        Me.POYREPBCODEDRUM.Name = "POYREPBCODEDRUM"
        Me.POYREPBCODEDRUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.POYREPBCODEDRUM.Width = 130
        '
        'POYPACKDATE
        '
        Me.POYPACKDATE.HeaderText = "Pack Date"
        Me.POYPACKDATE.Name = "POYPACKDATE"
        Me.POYPACKDATE.Visible = False
        '
        'frmChangeDrums
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(710, 561)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.DGVChageDrum)
        Me.Name = "frmChangeDrums"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Drums"
        CType(Me.DGVChageDrum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVChageDrum As DataGridView
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnCheck As Button
    Friend WithEvents POYPACKIDX As DataGridViewTextBoxColumn
    Friend WithEvents POYSTEPNUM As DataGridViewTextBoxColumn
    Friend WithEvents POYBCODEDRUM As DataGridViewTextBoxColumn
    Friend WithEvents POYREPBCODEDRUM As DataGridViewTextBoxColumn
    Friend WithEvents POYPACKDATE As DataGridViewTextBoxColumn
End Class
