﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSortJobDisplay
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSortJobDisplay))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.DGVDisplays = New System.Windows.Forms.DataGridView()
        Me.poystate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymccode = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DGVTmp = New System.Windows.Forms.DataGridView()
        Me.DGVTmp2 = New System.Windows.Forms.DataGridView()
        Me.tmrUpdateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnRelease = New System.Windows.Forms.Button()
        CType(Me.DGVDisplays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.DGVTmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVTmp2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(755, 578)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 44)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'DGVDisplays
        '
        Me.DGVDisplays.AllowUserToAddRows = False
        Me.DGVDisplays.AllowUserToDeleteRows = False
        Me.DGVDisplays.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVDisplays.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVDisplays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDisplays.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.poystate, Me.poymccode, Me.poymcnum, Me.poyprodname, Me.poymergenum, Me.poyprodweight, Me.poydoffnum, Me.poyGradeA, Me.poyGradeAB, Me.gradeShort, Me.gradeShortAB, Me.missing, Me.poySortStartTM, Me.poySortEndTM, Me.poycartcount})
        Me.DGVDisplays.Location = New System.Drawing.Point(0, 3)
        Me.DGVDisplays.Name = "DGVDisplays"
        Me.DGVDisplays.ReadOnly = True
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.NullValue = "--"
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVDisplays.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DGVDisplays.RowHeadersVisible = False
        Me.DGVDisplays.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVDisplays.Size = New System.Drawing.Size(1542, 531)
        Me.DGVDisplays.TabIndex = 0
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
        'poymccode
        '
        Me.poymccode.DividerWidth = 5
        Me.poymccode.HeaderText = "MC Code"
        Me.poymccode.Name = "poymccode"
        Me.poymccode.ReadOnly = True
        Me.poymccode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
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
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DGVDisplays)
        Me.Panel2.Location = New System.Drawing.Point(12, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1505, 552)
        Me.Panel2.TabIndex = 1
        '
        'DGVTmp
        '
        Me.DGVTmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTmp.Location = New System.Drawing.Point(12, 639)
        Me.DGVTmp.Name = "DGVTmp"
        Me.DGVTmp.Size = New System.Drawing.Size(916, 281)
        Me.DGVTmp.TabIndex = 3
        '
        'DGVTmp2
        '
        Me.DGVTmp2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTmp2.Location = New System.Drawing.Point(966, 639)
        Me.DGVTmp2.Name = "DGVTmp2"
        Me.DGVTmp2.Size = New System.Drawing.Size(926, 281)
        Me.DGVTmp2.TabIndex = 4
        '
        'tmrUpdateTimer
        '
        Me.tmrUpdateTimer.Interval = 10000
        '
        'btnRelease
        '
        Me.btnRelease.BackColor = System.Drawing.Color.GreenYellow
        Me.btnRelease.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRelease.Location = New System.Drawing.Point(78, 578)
        Me.btnRelease.Name = "btnRelease"
        Me.btnRelease.Size = New System.Drawing.Size(185, 44)
        Me.btnRelease.TabIndex = 5
        Me.btnRelease.Text = "Release Held Drums"
        Me.btnRelease.UseVisualStyleBackColor = False
        '
        'frmSortJobDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.DGVTmp)
        Me.Controls.Add(Me.btnRelease)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.DGVTmp2)
        Me.Controls.Add(Me.Panel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSortJobDisplay"
        Me.Text = "Jobs in Sorting"
        CType(Me.DGVDisplays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.DGVTmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVTmp2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As Button
    Friend WithEvents DGVDisplays As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DGVTmp As DataGridView
    Friend WithEvents DGVTmp2 As DataGridView
    Friend WithEvents tmrUpdateTimer As Timer
    Friend WithEvents poystate As DataGridViewTextBoxColumn
    Friend WithEvents poymccode As DataGridViewTextBoxColumn
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
    Friend WithEvents btnRelease As Button
End Class
