<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSortJobDetail
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DGVDoffTmp2 = New System.Windows.Forms.DataGridView()
        Me.DGVDoffTmp1 = New System.Windows.Forms.DataGridView()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DGVMcDoffInfo = New System.Windows.Forms.DataGridView()
        Me.poystate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymccode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymcnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poycartnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poyprodname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poymergenum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poydoffnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poygradeA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poygradeAB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gradeshort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gradeshortAB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.missing = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.poysortendtm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DGVDoffTmp2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVDoffTmp1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVMcDoffInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGVDoffTmp2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGVDoffTmp1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DGVMcDoffInfo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1496, 607)
        Me.SplitContainer1.SplitterDistance = 188
        Me.SplitContainer1.TabIndex = 0
        '
        'DGVDoffTmp2
        '
        Me.DGVDoffTmp2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDoffTmp2.Location = New System.Drawing.Point(12, 359)
        Me.DGVDoffTmp2.Name = "DGVDoffTmp2"
        Me.DGVDoffTmp2.Size = New System.Drawing.Size(160, 150)
        Me.DGVDoffTmp2.TabIndex = 5
        '
        'DGVDoffTmp1
        '
        Me.DGVDoffTmp1.AllowUserToAddRows = False
        Me.DGVDoffTmp1.AllowUserToDeleteRows = False
        Me.DGVDoffTmp1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDoffTmp1.Location = New System.Drawing.Point(12, 208)
        Me.DGVDoffTmp1.Name = "DGVDoffTmp1"
        Me.DGVDoffTmp1.ReadOnly = True
        Me.DGVDoffTmp1.Size = New System.Drawing.Size(173, 95)
        Me.DGVDoffTmp1.TabIndex = 4
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(3, 569)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(185, 35)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.YellowGreen
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(3, 167)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(185, 35)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "RELEASE HOLD"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DarkRed
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button2.Location = New System.Drawing.Point(3, 126)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(185, 35)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "HOLD"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LimeGreen
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(3, 33)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(185, 35)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "RELEASE TO PACK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'DGVMcDoffInfo
        '
        Me.DGVMcDoffInfo.AllowUserToAddRows = False
        Me.DGVMcDoffInfo.AllowUserToDeleteRows = False
        Me.DGVMcDoffInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMcDoffInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.poystate, Me.poymccode, Me.poymcnum, Me.poycartnum, Me.poyprodname, Me.poymergenum, Me.poydoffnum, Me.poygradeA, Me.poygradeAB, Me.gradeshort, Me.gradeshortAB, Me.missing, Me.poysortendtm})
        Me.DGVMcDoffInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMcDoffInfo.Location = New System.Drawing.Point(0, 0)
        Me.DGVMcDoffInfo.Name = "DGVMcDoffInfo"
        Me.DGVMcDoffInfo.ReadOnly = True
        Me.DGVMcDoffInfo.RowHeadersVisible = False
        Me.DGVMcDoffInfo.ShowCellToolTips = False
        Me.DGVMcDoffInfo.Size = New System.Drawing.Size(1304, 607)
        Me.DGVMcDoffInfo.TabIndex = 0
        '
        'poystate
        '
        Me.poystate.DividerWidth = 5
        Me.poystate.HeaderText = "STATE"
        Me.poystate.MaxInputLength = 5
        Me.poystate.Name = "poystate"
        Me.poystate.ReadOnly = True
        Me.poystate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poymccode
        '
        Me.poymccode.DividerWidth = 5
        Me.poymccode.HeaderText = "MC Code"
        Me.poymccode.MaxInputLength = 4
        Me.poymccode.Name = "poymccode"
        Me.poymccode.ReadOnly = True
        Me.poymccode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poymcnum
        '
        Me.poymcnum.DividerWidth = 5
        Me.poymcnum.HeaderText = "MC No."
        Me.poymcnum.MaxInputLength = 4
        Me.poymcnum.Name = "poymcnum"
        Me.poymcnum.ReadOnly = True
        Me.poymcnum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poycartnum
        '
        Me.poycartnum.DividerWidth = 5
        Me.poycartnum.HeaderText = "CART No."
        Me.poycartnum.MaxInputLength = 4
        Me.poycartnum.Name = "poycartnum"
        Me.poycartnum.ReadOnly = True
        Me.poycartnum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poyprodname
        '
        Me.poyprodname.DividerWidth = 5
        Me.poyprodname.HeaderText = "Product Kind"
        Me.poyprodname.MaxInputLength = 30
        Me.poyprodname.Name = "poyprodname"
        Me.poyprodname.ReadOnly = True
        Me.poyprodname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poymergenum
        '
        Me.poymergenum.DividerWidth = 5
        Me.poymergenum.HeaderText = "TF "
        Me.poymergenum.MaxInputLength = 4
        Me.poymergenum.Name = "poymergenum"
        Me.poymergenum.ReadOnly = True
        Me.poymergenum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poydoffnum
        '
        Me.poydoffnum.DividerWidth = 5
        Me.poydoffnum.HeaderText = "DOFF"
        Me.poydoffnum.MaxInputLength = 4
        Me.poydoffnum.Name = "poydoffnum"
        Me.poydoffnum.ReadOnly = True
        Me.poydoffnum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poygradeA
        '
        Me.poygradeA.DividerWidth = 5
        Me.poygradeA.HeaderText = "A"
        Me.poygradeA.MaxInputLength = 4
        Me.poygradeA.Name = "poygradeA"
        Me.poygradeA.ReadOnly = True
        Me.poygradeA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poygradeAB
        '
        Me.poygradeAB.DividerWidth = 5
        Me.poygradeAB.HeaderText = "AB"
        Me.poygradeAB.MaxInputLength = 4
        Me.poygradeAB.Name = "poygradeAB"
        Me.poygradeAB.ReadOnly = True
        Me.poygradeAB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'gradeshort
        '
        Me.gradeshort.DividerWidth = 5
        Me.gradeshort.HeaderText = "SHORT"
        Me.gradeshort.MaxInputLength = 4
        Me.gradeshort.Name = "gradeshort"
        Me.gradeshort.ReadOnly = True
        Me.gradeshort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'gradeshortAB
        '
        Me.gradeshortAB.DividerWidth = 5
        Me.gradeshortAB.HeaderText = "SAB"
        Me.gradeshortAB.MaxInputLength = 4
        Me.gradeshortAB.Name = "gradeshortAB"
        Me.gradeshortAB.ReadOnly = True
        Me.gradeshortAB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'missing
        '
        Me.missing.DividerWidth = 5
        Me.missing.HeaderText = "MISS"
        Me.missing.MaxInputLength = 4
        Me.missing.Name = "missing"
        Me.missing.ReadOnly = True
        Me.missing.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'poysortendtm
        '
        Me.poysortendtm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.poysortendtm.DividerWidth = 5
        Me.poysortendtm.HeaderText = "Sort End Time"
        Me.poysortendtm.Name = "poysortendtm"
        Me.poysortendtm.ReadOnly = True
        Me.poysortendtm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.poysortendtm.Width = 85
        '
        'frmSortJobDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1496, 607)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSortJobDetail"
        Me.Text = "Machine Cart Detail"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DGVDoffTmp2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVDoffTmp1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVMcDoffInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DGVMcDoffInfo As DataGridView
    Friend WithEvents btnCancel As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents DGVDoffTmp1 As DataGridView
    Friend WithEvents DGVDoffTmp2 As DataGridView
    Friend WithEvents poystate As DataGridViewTextBoxColumn
    Friend WithEvents poymccode As DataGridViewTextBoxColumn
    Friend WithEvents poymcnum As DataGridViewTextBoxColumn
    Friend WithEvents poycartnum As DataGridViewTextBoxColumn
    Friend WithEvents poyprodname As DataGridViewTextBoxColumn
    Friend WithEvents poymergenum As DataGridViewTextBoxColumn
    Friend WithEvents poydoffnum As DataGridViewTextBoxColumn
    Friend WithEvents poygradeA As DataGridViewTextBoxColumn
    Friend WithEvents poygradeAB As DataGridViewTextBoxColumn
    Friend WithEvents gradeshort As DataGridViewTextBoxColumn
    Friend WithEvents gradeshortAB As DataGridViewTextBoxColumn
    Friend WithEvents missing As DataGridViewTextBoxColumn
    Friend WithEvents poysortendtm As DataGridViewTextBoxColumn
End Class
