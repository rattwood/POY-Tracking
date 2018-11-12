<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTraceSearch
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnJobSearch = New System.Windows.Forms.Button()
        Me.txtTraceNum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(30, 143)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(678, 373)
        Me.DataGridView1.TabIndex = 88
        Me.DataGridView1.Visible = False
        '
        'btnJobSearch
        '
        Me.btnJobSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnJobSearch.Location = New System.Drawing.Point(559, 59)
        Me.btnJobSearch.Name = "btnJobSearch"
        Me.btnJobSearch.Size = New System.Drawing.Size(118, 47)
        Me.btnJobSearch.TabIndex = 69
        Me.btnJobSearch.Text = "Search"
        Me.btnJobSearch.UseVisualStyleBackColor = True
        '
        'txtTraceNum
        '
        Me.txtTraceNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTraceNum.Location = New System.Drawing.Point(198, 65)
        Me.txtTraceNum.Name = "txtTraceNum"
        Me.txtTraceNum.Size = New System.Drawing.Size(285, 29)
        Me.txtTraceNum.TabIndex = 66
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(40, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 20)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Trace Barcode #"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(295, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(275, 24)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "Search for Trace Information"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Salmon
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(884, 59)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(118, 47)
        Me.btnCancel.TabIndex = 89
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmTraceSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1410, 667)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnJobSearch)
        Me.Controls.Add(Me.txtTraceNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmTraceSearch"
        Me.Text = "Trace Search"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnJobSearch As Button
    Friend WithEvents txtTraceNum As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCancel As Button
End Class
