Public Class Usuarios

    Dim func As New UsuarioFunciones()
    Dim idSeleccionado As Integer = 0

    Private Sub EstadoBotones(ByVal modoEdicion As Boolean)

        btnGuardar.Enabled = Not modoEdicion
        btnEditar.Enabled = modoEdicion
        btnEliminar.Enabled = modoEdicion

        cbEstado.Enabled = modoEdicion

        If Not modoEdicion Then
            cbEstado.SelectedIndex = 0
            idSeleccionado = 0
        End If
    End Sub

    Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EstadoBotones(False)
        CargarUsuarios()
    End Sub

    Private Sub dgvUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsuarios.CellClick
        If e.RowIndex >= 0 Then
            Dim fila = dgvUsuarios.Rows(e.RowIndex)

            idSeleccionado = CInt(fila.Cells("ID").Value)
            txtNombre.Text = fila.Cells("NombreCompleto").Value.ToString()
            txtUser.Text = fila.Cells("Username").Value.ToString()
            txtPassword.Text = fila.Cells("Password").Value.ToString()
            cbEstado.Text = fila.Cells("Estado").Value.ToString()

            EstadoBotones(True)
        End If
    End Sub

    Private Sub dgvUsuarios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvUsuarios.CellFormatting
        If dgvUsuarios.Columns(e.ColumnIndex).Name = "Password" AndAlso e.Value IsNot Nothing Then
            e.Value = New String("*"c, e.Value.ToString().Length)
        End If
    End Sub

    Private Sub CargarUsuarios()
        Try
            Dim dt As DataTable = func.ObtenerUsuarios()
            dgvUsuarios.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error al cargar usuarios: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Dim EstadoBit As Boolean = If(cbEstado.Text = "Activo", True, False)

        Try
            If func.EditarUsuario(idSeleccionado, txtNombre.Text, txtUser.Text, txtPassword.Text, EstadoBit) Then
                MessageBox.Show("Usuario editado correctamente.")
                CargarUsuarios()
                EstadoBotones(False)
                LimpiarCampos()
            Else
                MessageBox.Show("Error al editar el usuario.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error al conectar a la base de datos: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If String.IsNullOrWhiteSpace(txtNombre.Text) OrElse String.IsNullOrWhiteSpace(txtUser.Text) OrElse String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Por favor, complete todos los campos.")
            Return
        End If

        If func.AgregarUsuario(txtNombre.Text, txtUser.Text, txtPassword.Text) Then
            MessageBox.Show("Usuario agregado correctamente.")
            CargarUsuarios()
            EstadoBotones(False)
            LimpiarCampos()
        Else
            MessageBox.Show("Error al agregar el usuario.")
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim result As DialogResult = MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            Try
                If func.EliminarUsuario(idSeleccionado) Then
                    MessageBox.Show("Usuario eliminado correctamente.")
                    CargarUsuarios()
                    EstadoBotones(False)
                    LimpiarCampos()
                Else
                    MessageBox.Show("Error al eliminar el usuario.")
                End If
            Catch ex As Exception
                MessageBox.Show("Error al conectar a la base de datos: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        LimpiarCampos()
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        dgvUsuarios.DataSource = func.ObtenerUsuarios(txtBuscar.Text)

        If dgvUsuarios.Columns.Contains("Password") Then
            dgvUsuarios.Columns("Password").Visible = False
        End If
    End Sub

    Private Sub LimpiarCampos()
        txtNombre.Clear()
        txtUser.Clear()
        txtPassword.Clear()
        cbEstado.SelectedIndex = 0
        idSeleccionado = 0
        EstadoBotones(False)
    End Sub

    Private Sub EstilizarBotones()
        For Each btn As Button In {btnGuardar, btnEditar, btnEliminar, btnLimpiar}
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
        Next
    End Sub

End Class