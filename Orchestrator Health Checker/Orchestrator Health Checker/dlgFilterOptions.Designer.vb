<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgFilterOptions
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.rbtRunbookServer = New System.Windows.Forms.RadioButton()
    Me.rbtRunbookName = New System.Windows.Forms.RadioButton()
    Me.cmbRunbookServer = New System.Windows.Forms.ComboBox()
    Me.txtRunbookName = New System.Windows.Forms.TextBox()
    Me.rbtNoFilter = New System.Windows.Forms.RadioButton()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(238, 148)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Cancel"
    '
    'rbtRunbookServer
    '
    Me.rbtRunbookServer.AutoSize = True
    Me.rbtRunbookServer.Location = New System.Drawing.Point(13, 50)
    Me.rbtRunbookServer.Name = "rbtRunbookServer"
    Me.rbtRunbookServer.Size = New System.Drawing.Size(116, 17)
    Me.rbtRunbookServer.TabIndex = 1
    Me.rbtRunbookServer.Text = "Runbook Server IS"
    Me.rbtRunbookServer.UseVisualStyleBackColor = True
    '
    'rbtRunbookName
    '
    Me.rbtRunbookName.AutoSize = True
    Me.rbtRunbookName.Location = New System.Drawing.Point(13, 88)
    Me.rbtRunbookName.Name = "rbtRunbookName"
    Me.rbtRunbookName.Size = New System.Drawing.Size(158, 17)
    Me.rbtRunbookName.TabIndex = 2
    Me.rbtRunbookName.Text = "Runbook Name CONTAINS"
    Me.rbtRunbookName.UseVisualStyleBackColor = True
    '
    'cmbRunbookServer
    '
    Me.cmbRunbookServer.Enabled = False
    Me.cmbRunbookServer.FormattingEnabled = True
    Me.cmbRunbookServer.Location = New System.Drawing.Point(135, 48)
    Me.cmbRunbookServer.Name = "cmbRunbookServer"
    Me.cmbRunbookServer.Size = New System.Drawing.Size(244, 21)
    Me.cmbRunbookServer.TabIndex = 3
    '
    'txtRunbookName
    '
    Me.txtRunbookName.Enabled = False
    Me.txtRunbookName.Location = New System.Drawing.Point(177, 86)
    Me.txtRunbookName.Name = "txtRunbookName"
    Me.txtRunbookName.Size = New System.Drawing.Size(202, 20)
    Me.txtRunbookName.TabIndex = 4
    '
    'rbtNoFilter
    '
    Me.rbtNoFilter.AutoSize = True
    Me.rbtNoFilter.Checked = True
    Me.rbtNoFilter.Location = New System.Drawing.Point(13, 12)
    Me.rbtNoFilter.Name = "rbtNoFilter"
    Me.rbtNoFilter.Size = New System.Drawing.Size(64, 17)
    Me.rbtNoFilter.TabIndex = 5
    Me.rbtNoFilter.TabStop = True
    Me.rbtNoFilter.Text = "No Filter"
    Me.rbtNoFilter.UseVisualStyleBackColor = True
    '
    'dlgFilterOptions
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(396, 189)
    Me.Controls.Add(Me.rbtNoFilter)
    Me.Controls.Add(Me.txtRunbookName)
    Me.Controls.Add(Me.cmbRunbookServer)
    Me.Controls.Add(Me.rbtRunbookName)
    Me.Controls.Add(Me.rbtRunbookServer)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgFilterOptions"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Filter Options"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents rbtRunbookServer As System.Windows.Forms.RadioButton
  Friend WithEvents rbtRunbookName As System.Windows.Forms.RadioButton
  Friend WithEvents cmbRunbookServer As System.Windows.Forms.ComboBox
  Friend WithEvents txtRunbookName As System.Windows.Forms.TextBox
  Friend WithEvents rbtNoFilter As System.Windows.Forms.RadioButton

End Class
