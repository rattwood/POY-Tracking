<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSortJobDisplay
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.DGVDisplays = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DGVTmp2 = New System.Windows.Forms.DataGridView()
        Me.DGVTmp = New System.Windows.Forms.DataGridView()
        Me.DGVTmp3 = New System.Windows.Forms.DataGridView()
        Me.poystate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymcnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poyprodname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymergenum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poyprodweight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poydoffnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poyGradeA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poyGradeAB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gradeShort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gradeShortAB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.missing = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poySortStartTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poySortEndTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poycartcount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGVDisplays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.DGVTmp2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVTmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVTmp3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(39, 578)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 44)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'DGVDisplays
        '
        Me.DGVDisplays.AllowUserToAddRows = False
        Me.DGVDisplays.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVDisplays.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVDisplays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDisplays.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.poystate, Me.poymcnum, Me.poyprodname, Me.poymergenum, Me.poyprodweight, Me.poydoffnum, Me.poyGradeA, Me.poyGradeAB, Me.gradeShort, Me.gradeShortAB, Me.missing, Me.poySortStartTM, Me.poySortEndTM, Me.poycartcount})
        Me.DGVDisplays.Location = New System.Drawing.Point(3, 3)
        Me.DGVDisplays.Name = "DGVDisplays"
        Me.DGVDisplays.ReadOnly = True
        Me.DGVDisplays.RowHeadersVisible = False
        Me.DGVDisplays.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVDisplays.Size = New System.Drawing.Size(1476, 552)
        Me.DGVDisplays.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DGVDisplays)
        Me.Panel2.Location = New System.Drawing.Point(12, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1482, 552)
        Me.Panel2.TabIndex = 1
        '
        'DGVTmp2
        '
        Me.DGVTmp2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTmp2.Location = New System.Drawing.Point(1472, 88)
        Me.DGVTmp2.Name = "DGVTmp2"
        Me.DGVTmp2.Size = New System.Drawing.Size(79, 100)
        Me.DGVTmp2.TabIndex = 4
        Me.DGVTmp2.Visible = False
        '
        'DGVTmp
        '
        Me.DGVTmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTmp.Location = New System.Drawing.Point(1472, 209)
        Me.DGVTmp.Name = "DGVTmp"
        Me.DGVTmp.Size = New System.Drawing.Size(89, 83)
        Me.DGVTmp.TabIndex = 3
        Me.DGVTmp.Visible = False
        '
        'DGVTmp3
        '
        Me.DGVTmp3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTmp3.Location = New System.Drawing.Point(1480, 328)
        Me.DGVTmp3.Name = "DGVTmp3"
        Me.DGVTmp3.Size = New System.Drawing.Size(71, 54)
        Me.DGVTmp3.TabIndex = 3
        Me.DGVTmp3.Visible = False
        '
        'poystate
        '
        Me.poystate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.poystate.DefaultCellStyle = DataGridViewCellStyle2
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
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.poyprodweight.DefaultCellStyle = DataGridViewCellStyle3
        Me.poyprodweight.DividerWidth = 5
        Me.poyprodweight.HeaderText = "CODE"
        Me.poyprodweight.MaxInputLength = 5
        Me.poyprodweight.Name = "poyprodweight"
        Me.poyprodweight.ReadOnly = True
        Me.poyprodweight.Width = 80
        '
        'poydoffnum
        '
        Me.poydoffnum.DividerWidth = 5
        Me.poydoffnum.HeaderText = "DOFF No."
        Me.poydoffnum.Name = "poydoffnum"
        Me.poydoffnum.ReadOnly = True
        '
        'poyGradeA
        '
        Me.poyGradeA.DividerWidth = 5
        Me.poyGradeA.HeaderText = "A"
        Me.poyGradeA.Name = "poyGradeA"
        Me.poyGradeA.ReadOnly = True
        '
        'poyGradeAB
        '
        Me.poyGradeAB.DividerWidth = 5
        Me.poyGradeAB.HeaderText = "AB"
        Me.poyGradeAB.Name = "poyGradeAB"
        Me.poyGradeAB.ReadOnly = True
        '
        'gradeShort
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.gradeShort.DefaultCellStyle = DataGridViewCellStyle4
        Me.gradeShort.DividerWidth = 5
        Me.gradeShort.HeaderText = "S"
        Me.gradeShort.MaxInputLength = 5
        Me.gradeShort.Name = "gradeShort"
        Me.gradeShort.ReadOnly = True
        '
        'gradeShortAB
        '
        Me.gradeShortAB.DividerWidth = 5
        Me.gradeShortAB.HeaderText = "SAB"
        Me.gradeShortAB.Name = "gradeShortAB"
        Me.gradeShortAB.ReadOnly = True
        '
        'missing
        '
        Me.missing.DividerWidth = 5
        Me.missing.HeaderText = "MISS"
        Me.missing.Name = "missing"
        Me.missing.ReadOnly = True
        '
        'poySortStartTM
        '
        Me.poySortStartTM.DividerWidth = 5
        Me.poySortStartTM.HeaderText = "SORT START"
        Me.poySortStartTM.Name = "poySortStartTM"
        Me.poySortStartTM.ReadOnly = True
        Me.poySortStartTM.Width = 150
        '
        'poySortEndTM
        '
        Me.poySortEndTM.DividerWidth = 5
        Me.poySortEndTM.HeaderText = "SORT END"
        Me.poySortEndTM.Name = "poySortEndTM"
        Me.poySortEndTM.ReadOnly = True
        Me.poySortEndTM.Width = 150
        '
        'poycartcount
        '
        Me.poycartcount.DividerWidth = 5
        Me.poycartcount.HeaderText = "CART COUNT"
        Me.poycartcount.Name = "poycartcount"
        Me.poycartcount.ReadOnly = True
        Me.poycartcount.Width = 130
        '
        'frmSortJobDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1563, 634)
        Me.Controls.Add(Me.DGVTmp3)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.DGVTmp)
        Me.Controls.Add(Me.DGVTmp2)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmSortJobDisplay"
        Me.Text = "frmSortJobDisplay"
        CType(Me.DGVDisplays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.DGVTmp2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVTmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVTmp3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As Button
    Friend WithEvents DGVDisplays As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DGVTmp As DataGridView
    Friend WithEvents DGVTmp2 As DataGridView
    Friend WithEvents DGVTmp3 As DataGridView
    Friend WithEvents poystate As DataGridViewTextBoxColumn
    Friend WithEvents poymcnum As DataGridViewTextBoxColumn
    Friend WithEvents poyprodname As DataGridViewTextBoxColumn
    Friend WithEvents poymergenum As DataGridViewTextBoxColumn
    Friend WithEvents poyprodweight As DataGridViewTextBoxColumn
    Friend WithEvents poydoffnum As DataGridViewTextBoxColumn
    Friend WithEvents poyGradeA As DataGridViewTextBoxColumn
    Friend WithEvents poyGradeAB As DataGridViewTextBoxColumn
    Friend WithEvents gradeShort As DataGridViewTextBoxColumn
    Friend WithEvents gradeShortAB As DataGridViewTextBoxColumn
    Friend WithEvents missing As DataGridViewTextBoxColumn
    Friend WithEvents poySortStartTM As DataGridViewTextBoxColumn
    Friend WithEvents poySortEndTM As DataGridViewTextBoxColumn
    Friend WithEvents poycartcount As DataGridViewTextBoxColumn
End Class
