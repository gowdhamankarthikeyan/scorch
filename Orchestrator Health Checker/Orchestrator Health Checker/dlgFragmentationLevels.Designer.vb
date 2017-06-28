<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgFragmentationLevels
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
    Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.chkFilter = New System.Windows.Forms.CheckBox()
    Me.txtReOrg = New System.Windows.Forms.NumericUpDown()
    Me.txtRebuild = New System.Windows.Forms.NumericUpDown()
    Me.txtPages = New System.Windows.Forms.NumericUpDown()
    Me.worker = New System.ComponentModel.BackgroundWorker()
    Me.gridFragLevels = New System.Windows.Forms.DataGridView()
    Me.colSchema = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colTable = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colIndexType = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colAVGFrag = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colShouldRebuild = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colShouldReOrg = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colHasLOBs = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colObjectID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colIndexID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colPartitionNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
    Me.progressBar = New System.Windows.Forms.ToolStripProgressBar()
    Me.cmdShow = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdReIndexTables = New System.Windows.Forms.Button()
    CType(Me.txtReOrg, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.txtRebuild, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.txtPages, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.gridFragLevels, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.StatusStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(236, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Minimum fragmentation % to recommend Re-Org:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(13, 39)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(238, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Minimum fragmentation % to recommend Rebuild:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(382, 13)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(263, 13)
    Me.Label3.TabIndex = 3
    Me.Label3.Text = "Minimum # of Pages before you worry about the index:"
    '
    'chkFilter
    '
    Me.chkFilter.AutoSize = True
    Me.chkFilter.Checked = True
    Me.chkFilter.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkFilter.Location = New System.Drawing.Point(382, 37)
    Me.chkFilter.Name = "chkFilter"
    Me.chkFilter.Size = New System.Drawing.Size(231, 17)
    Me.chkFilter.TabIndex = 4
    Me.chkFilter.Text = "Only Show Reorg/Rebuild (Recommended)"
    Me.chkFilter.UseVisualStyleBackColor = True
    '
    'txtReOrg
    '
    Me.txtReOrg.Location = New System.Drawing.Point(255, 9)
    Me.txtReOrg.Name = "txtReOrg"
    Me.txtReOrg.Size = New System.Drawing.Size(60, 20)
    Me.txtReOrg.TabIndex = 5
    Me.txtReOrg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.txtReOrg.Value = New Decimal(New Integer() {15, 0, 0, 0})
    '
    'txtRebuild
    '
    Me.txtRebuild.Location = New System.Drawing.Point(255, 35)
    Me.txtRebuild.Name = "txtRebuild"
    Me.txtRebuild.Size = New System.Drawing.Size(60, 20)
    Me.txtRebuild.TabIndex = 6
    Me.txtRebuild.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.txtRebuild.Value = New Decimal(New Integer() {30, 0, 0, 0})
    '
    'txtPages
    '
    Me.txtPages.Location = New System.Drawing.Point(652, 9)
    Me.txtPages.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
    Me.txtPages.Name = "txtPages"
    Me.txtPages.Size = New System.Drawing.Size(60, 20)
    Me.txtPages.TabIndex = 7
    Me.txtPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.txtPages.ThousandsSeparator = True
    Me.txtPages.Value = New Decimal(New Integer() {10, 0, 0, 0})
    '
    'worker
    '
    '
    'gridFragLevels
    '
    Me.gridFragLevels.AllowUserToAddRows = False
    Me.gridFragLevels.AllowUserToDeleteRows = False
    Me.gridFragLevels.AllowUserToOrderColumns = True
    Me.gridFragLevels.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridFragLevels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridFragLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridFragLevels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSchema, Me.colTable, Me.colIndex, Me.colIndexType, Me.colAVGFrag, Me.colShouldRebuild, Me.colShouldReOrg, Me.colHasLOBs, Me.colObjectID, Me.colIndexID, Me.colPartitionNumber})
    Me.gridFragLevels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridFragLevels.Location = New System.Drawing.Point(12, 65)
    Me.gridFragLevels.Name = "gridFragLevels"
    Me.gridFragLevels.ReadOnly = True
    Me.gridFragLevels.RowHeadersVisible = False
    Me.gridFragLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridFragLevels.Size = New System.Drawing.Size(983, 398)
    Me.gridFragLevels.TabIndex = 9
    '
    'colSchema
    '
    Me.colSchema.DataPropertyName = "Schema Name"
    Me.colSchema.HeaderText = "Schema Name"
    Me.colSchema.Name = "colSchema"
    Me.colSchema.ReadOnly = True
    '
    'colTable
    '
    Me.colTable.DataPropertyName = "Table Name"
    Me.colTable.HeaderText = "Table Name"
    Me.colTable.Name = "colTable"
    Me.colTable.ReadOnly = True
    '
    'colIndex
    '
    Me.colIndex.DataPropertyName = "Index Name"
    Me.colIndex.HeaderText = "Index Name"
    Me.colIndex.Name = "colIndex"
    Me.colIndex.ReadOnly = True
    '
    'colIndexType
    '
    Me.colIndexType.DataPropertyName = "Index Type"
    Me.colIndexType.HeaderText = "Index Type"
    Me.colIndexType.Name = "colIndexType"
    Me.colIndexType.ReadOnly = True
    '
    'colAVGFrag
    '
    Me.colAVGFrag.DataPropertyName = "Avg Frag (%)"
    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    DataGridViewCellStyle1.Format = "# ""%"""
    DataGridViewCellStyle1.NullValue = Nothing
    Me.colAVGFrag.DefaultCellStyle = DataGridViewCellStyle1
    Me.colAVGFrag.HeaderText = "AVG Frag"
    Me.colAVGFrag.Name = "colAVGFrag"
    Me.colAVGFrag.ReadOnly = True
    '
    'colShouldRebuild
    '
    Me.colShouldRebuild.DataPropertyName = "Should Rebuild"
    DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    Me.colShouldRebuild.DefaultCellStyle = DataGridViewCellStyle2
    Me.colShouldRebuild.HeaderText = "Should Rebuild?"
    Me.colShouldRebuild.Name = "colShouldRebuild"
    Me.colShouldRebuild.ReadOnly = True
    '
    'colShouldReOrg
    '
    Me.colShouldReOrg.DataPropertyName = "Should Reorg"
    DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    Me.colShouldReOrg.DefaultCellStyle = DataGridViewCellStyle3
    Me.colShouldReOrg.HeaderText = "Should Re-Org?"
    Me.colShouldReOrg.Name = "colShouldReOrg"
    Me.colShouldReOrg.ReadOnly = True
    '
    'colHasLOBs
    '
    Me.colHasLOBs.DataPropertyName = "Has LOBs"
    DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    Me.colHasLOBs.DefaultCellStyle = DataGridViewCellStyle4
    Me.colHasLOBs.HeaderText = "Has LOBs?"
    Me.colHasLOBs.Name = "colHasLOBs"
    Me.colHasLOBs.ReadOnly = True
    '
    'colObjectID
    '
    Me.colObjectID.DataPropertyName = "Object ID"
    DataGridViewCellStyle5.NullValue = Nothing
    Me.colObjectID.DefaultCellStyle = DataGridViewCellStyle5
    Me.colObjectID.HeaderText = "Object ID"
    Me.colObjectID.Name = "colObjectID"
    Me.colObjectID.ReadOnly = True
    '
    'colIndexID
    '
    Me.colIndexID.DataPropertyName = "Index ID"
    Me.colIndexID.HeaderText = "Index ID"
    Me.colIndexID.Name = "colIndexID"
    Me.colIndexID.ReadOnly = True
    '
    'colPartitionNumber
    '
    Me.colPartitionNumber.DataPropertyName = "Partition Number"
    Me.colPartitionNumber.HeaderText = "Partition Number"
    Me.colPartitionNumber.Name = "colPartitionNumber"
    Me.colPartitionNumber.ReadOnly = True
    '
    'StatusStrip1
    '
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.progressBar})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 466)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(1007, 22)
    Me.StatusStrip1.TabIndex = 10
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'lblStatus
    '
    Me.lblStatus.Name = "lblStatus"
    Me.lblStatus.Size = New System.Drawing.Size(77, 17)
    Me.lblStatus.Text = "Status: Ready"
    '
    'progressBar
    '
    Me.progressBar.Enabled = False
    Me.progressBar.Name = "progressBar"
    Me.progressBar.Size = New System.Drawing.Size(100, 16)
    Me.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
    Me.progressBar.Value = 20
    Me.progressBar.Visible = False
    '
    'cmdShow
    '
    Me.cmdShow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdShow.Image = Global.Orchestrator_Health_Checker.My.Resources.Resources.Run
    Me.cmdShow.Location = New System.Drawing.Point(734, 8)
    Me.cmdShow.Name = "cmdShow"
    Me.cmdShow.Size = New System.Drawing.Size(75, 46)
    Me.cmdShow.TabIndex = 8
    Me.cmdShow.Tag = "Show"
    Me.cmdShow.Text = "Show"
    Me.cmdShow.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdClose.Image = Global.Orchestrator_Health_Checker.My.Resources.Resources.Close
    Me.cmdClose.Location = New System.Drawing.Point(920, 8)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(75, 46)
    Me.cmdClose.TabIndex = 11
    Me.cmdClose.Text = "Close"
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdReIndexTables
    '
    Me.cmdReIndexTables.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdReIndexTables.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdReIndexTables.Image = Global.Orchestrator_Health_Checker.My.Resources.Resources.Wipe
    Me.cmdReIndexTables.Location = New System.Drawing.Point(827, 8)
    Me.cmdReIndexTables.Name = "cmdReIndexTables"
    Me.cmdReIndexTables.Size = New System.Drawing.Size(75, 46)
    Me.cmdReIndexTables.TabIndex = 12
    Me.cmdReIndexTables.Text = "ReIndex"
    Me.cmdReIndexTables.UseVisualStyleBackColor = True
    '
    'dlgFragmentationLevels
    '
    Me.AcceptButton = Me.cmdShow
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdClose
    Me.ClientSize = New System.Drawing.Size(1007, 488)
    Me.Controls.Add(Me.cmdReIndexTables)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.gridFragLevels)
    Me.Controls.Add(Me.cmdShow)
    Me.Controls.Add(Me.txtPages)
    Me.Controls.Add(Me.txtRebuild)
    Me.Controls.Add(Me.txtReOrg)
    Me.Controls.Add(Me.chkFilter)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgFragmentationLevels"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Database - Fragmentation Levels"
    CType(Me.txtReOrg, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.txtRebuild, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.txtPages, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.gridFragLevels, System.ComponentModel.ISupportInitialize).EndInit()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents chkFilter As System.Windows.Forms.CheckBox
  Friend WithEvents txtReOrg As System.Windows.Forms.NumericUpDown
  Friend WithEvents txtRebuild As System.Windows.Forms.NumericUpDown
  Friend WithEvents txtPages As System.Windows.Forms.NumericUpDown
  Friend WithEvents cmdShow As System.Windows.Forms.Button
  Friend WithEvents worker As System.ComponentModel.BackgroundWorker
  Friend WithEvents gridFragLevels As System.Windows.Forms.DataGridView
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents progressBar As System.Windows.Forms.ToolStripProgressBar
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents colSchema As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colTable As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colIndex As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colIndexType As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colAVGFrag As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colShouldRebuild As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colShouldReOrg As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colHasLOBs As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colObjectID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colIndexID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colPartitionNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents cmdReIndexTables As System.Windows.Forms.Button

End Class
