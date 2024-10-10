' Database Programming Assignment
' Create a program that utilizes a database
' Josiah Barringer
' "I will not use code that was never covered in class or in our textbook. 
' If you do you must be able to explain your code when asked by the professor. 
' Using code outside of the resources provided, it can be flagged and 
' reported as an academic integrity violation if there is any suspicion 
' of copying/cheating."


Imports System.Data.SqlClient

Public Class Form1
    ' Connection string to the "Galore" database in the Debug folder
    Dim connString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Galore.mdf;Integrated Security=True"
    Dim conn As New SqlConnection(connString)
    Dim dsGalore As New DataSet()

    ' Method to load data from database and bind to textboxes
    Private Sub LoadData()
        Dim cmd As New SqlCommand("SELECT * FROM TableName", conn)
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(dsGalore, "TableName")


    End Sub

    ' Save button click event with Try-Catch for error handling
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Add save logic here (example: updating dataset back to database)
            MessageBox.Show("Changes saved successfully.")
        Catch ex As Exception
            MessageBox.Show("Error saving changes: " & ex.Message)
        End Try
    End Sub

    ' Query selection from ComboBox or buttons
    Private Sub cmbQuery_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbQuery.SelectedIndexChanged
        Dim query As String = ""

        Select Case cmbQuery.SelectedIndex
            Case 0
                ' Create table for user preferences
                query = "CREATE TABLE UserGamePreferences (" &
                "PreferenceID INT PRIMARY KEY IDENTITY(1,1), " &
                "UserID INT, GameTitle NVARCHAR(100), " &
                "Platform NVARCHAR(50), Rating NVARCHAR(10), PreferredPrice DECIMAL(5,2));"

            Case 1
                ' Insert new game into Games table
                query = "INSERT INTO Games (Title, Platform, Rating, Price, NewUsed, Quantity) " &
                "VALUES ('Cyberpunk 2077', 'XB', 'M', 49.99, 'N', 10);"

            Case 2
                ' Delete a game from the catalog
                query = "DELETE FROM Games WHERE Title = 'Madden NFL 20' AND Platform = 'PS';"

            Case 3
                ' Update game price
                query = "UPDATE Games SET Price = 29.99 WHERE Title = 'Resident Evil' AND Platform = 'PS';"

            Case 4
                ' Sort games by price
                query = "SELECT Title, Platform, Price FROM Games ORDER BY Price ASC;"
        End Select




        ' Execute query and update dataset
        Dim cmd As New SqlCommand(query, conn)
        Dim adapter As New SqlDataAdapter(cmd)
        dsGalore.Clear()
        adapter.Fill(dsGalore, "TableName")
    End Sub

    ' Close button event to close the application
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub


End Class


