<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackDisplay
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPackDisplay))
        Me.DGVPackDisplays = New System.Windows.Forms.DataGridView()
        Me.DGVTmp2 = New System.Windows.Forms.DataGridView()
        Me.DGVTmp = New System.Windows.Forms.DataGridView()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.TimerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.poystate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymcnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poyprodname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymergenum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poyprodweight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poycartcount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.drumCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pallet48 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pallet72 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pallet120 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HoldStartTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGVPackDisplays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVTmp2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVTmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVPackDisplays
        '
        Me.DGVPackDisplays.AllowUserToAddRows = False
        Me.DGVPackDisplays.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DGVPackDisplays.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVPackDisplays.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVPackDisplays.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVPackDisplays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVPackDisplays.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.poystate, Me.poymcnum, Me.poyprodname, Me.poymergenum, Me.poyprodweight, Me.poycartcount, Me.drumCount, Me.Pallet48, Me.Pallet72, Me.Pallet120, Me.HoldStartTime})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVPackDisplays.DefaultCellStyle = DataGridViewCellStyle7
        Me.DGVPackDisplays.Location = New System.Drawing.Point(6, 4)
        Me.DGVPackDisplays.Name = "DGVPackDisplays"
        Me.DGVPackDisplays.ReadOnly = True
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.NullValue = "--"
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVPackDisplays.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DGVPackDisplays.RowHeadersVisible = False
        Me.DGVPackDisplays.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVPackDisplays.Size = New System.Drawing.Size(1157, 426)
        Me.DGVPackDisplays.TabIndex = 5
        '
        'DGVTmp2
        '
        Me.DGVTmp2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTmp2.Location = New System.Drawing.Point(32, 447)
        Me.DGVTmp2.Name = "DGVTmp2"
        Me.DGVTmp2.Size = New System.Drawing.Size(152, 100)
        Me.DGVTmp2.TabIndex = 9
        Me.DGVTmp2.Visible = False
        '
        'DGVTmp
        '
        Me.DGVTmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTmp.Location = New System.Drawing.Point(835, 436)
        Me.DGVTmp.Name = "DGVTmp"
        Me.DGVTmp.Size = New System.Drawing.Size(304, 155)
        Me.DGVTmp.TabIndex = 8
        Me.DGVTmp.Visible = False
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Menu
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblMessage.Location = New System.Drawing.Point(571, 433)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(140, 24)
        Me.lblMessage.TabIndex = 6
        Me.lblMessage.Text = "Updating Data"
        Me.lblMessage.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.Location = New System.Drawing.Point(575, 503)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 44)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'TimerRefresh
        '
        Me.TimerRefresh.Interval = 10000
        '
        'poystate
        '
        Me.poystate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.poystate.DefaultCellStyle = DataGridViewCellStyle3
        Me.poystate.DividerWidth = 5
        Me.poystate.HeaderText = ""
        Me.poystate.MaxInputLength = 5
        Me.poystate.MinimumWidth = 20
        Me.poystate.Name = "poystate"
        Me.poystate.ReadOnly = True
        Me.poystate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.poystate.Width = 24
        '
        'poymcnum
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.poymcnum.DefaultCellStyle = DataGridViewCellStyle4
        Me.poymcnum.DividerWidth = 5
        Me.poymcnum.HeaderText = "MC no."
        Me.poymcnum.MaxInputLength = 3
        Me.poymcnum.Name = "poymcnum"
        Me.poymcnum.ReadOnly = True
        Me.poymcnum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.poymcnum.Width = 70
        '
        'poyprodname
        '
        Me.poyprodname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.poyprodname.DefaultCellStyle = DataGridViewCellStyle5
        Me.poyprodname.DividerWidth = 5
        Me.poyprodname.HeaderText = "PRODUCT KIND"
        Me.poyprodname.Name = "poyprodname"
        Me.poyprodname.ReadOnly = True
        Me.poyprodname.Width = 150
        '
        'poymergenum
        '
        Me.poymergenum.DividerWidth = 5
        Me.poymergenum.HeaderText = "TF "
        Me.poymergenum.Name = "poymergenum"
        Me.poymergenum.ReadOnly = True
        Me.poymergenum.Width = 50
        '
        'poyprodweight
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.poyprodweight.DefaultCellStyle = DataGridViewCellStyle6
        Me.poyprodweight.DividerWidth = 5
        Me.poyprodweight.HeaderText = "CODE"
        Me.poyprodweight.MaxInputLength = 5
        Me.poyprodweight.Name = "poyprodweight"
        Me.poyprodweight.ReadOnly = True
        Me.poyprodweight.Width = 80
        '
        'poycartcount
        '
        Me.poycartcount.DividerWidth = 5
        Me.poycartcount.HeaderText = "CART COUNT"
        Me.poycartcount.Name = "poycartcount"
        Me.poycartcount.ReadOnly = True
        Me.poycartcount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.poycartcount.Width = 130
        '
        'drumCount
        '
        Me.drumCount.DividerWidth = 5
        Me.drumCount.HeaderText = "DRUMS"
        Me.drumCount.Name = "drumCount"
        Me.drumCount.ReadOnly = True
        '
        'Pallet48
        '
        Me.Pallet48.DividerWidth = 5
        Me.Pallet48.HeaderText = "Pallet 48"
        Me.Pallet48.Name = "Pallet48"
        Me.Pallet48.ReadOnly = True
        '
        'Pallet72
        '
        Me.Pallet72.DividerWidth = 5
        Me.Pallet72.HeaderText = "Pallet 72"
        Me.Pallet72.Name = "Pallet72"
        Me.Pallet72.ReadOnly = True
        '
        'Pallet120
        '
        Me.Pallet120.DividerWidth = 5
        Me.Pallet120.HeaderText = "Pallet 120"
        Me.Pallet120.Name = "Pallet120"
        Me.Pallet120.ReadOnly = True
        '
        'HoldStartTime
        '
        Me.HoldStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.HoldStartTime.HeaderText = "Hold Time"
        Me.HoldStartTime.Name = "HoldStartTime"
        Me.HoldStartTime.ReadOnly = True
        Me.HoldStartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.HoldStartTime.Width = 150
        '
        'frmPackDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1163, 555)
        Me.Controls.Add(Me.DGVPackDisplays)
        Me.Controls.Add(Me.DGVTmp2)
        Me.Controls.Add(Me.DGVTmp)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPackDisplay"
        Me.Text = "Products for packing"
        CType(Me.DGVPackDisplays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVTmp2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVTmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVPackDisplays As DataGridView
    Friend WithEvents DGVTmp2 As DataGridView
    Friend WithEvents DGVTmp As DataGridView
    Friend WithEvents lblMessage As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents TimerRefresh As Timer
    Friend WithEvents poystate As DataGridViewTextBoxColumn
    Friend WithEvents poymcnum As DataGridViewTextBoxColumn
    Friend WithEvents poyprodname As DataGridViewTextBoxColumn
    Friend WithEvents poymergenum As DataGridViewTextBoxColumn
    Friend WithEvents poyprodweight As DataGridViewTextBoxColumn
    Friend WithEvents poycartcount As DataGridViewTextBoxColumn
    Friend WithEvents drumCount As DataGridViewTextBoxColumn
    Friend WithEvents Pallet48 As DataGridViewTextBoxColumn
    Friend WithEvents Pallet72 As DataGridViewTextBoxColumn
    Friend WithEvents Pallet120 As DataGridViewTextBoxColumn
    Friend WithEvents HoldStartTime As DataGridViewTextBoxColumn
End Class
