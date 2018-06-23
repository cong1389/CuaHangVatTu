Imports System.Text
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data

Namespace adv.Business
    Public Module modGeneral
        Public ConnectionString As String
        Public DBAccessType As enumDBAccessType = enumDBAccessType.oledb

        Public Enum Language
            Viet = 0
            Anh = 1
        End Enum

        Public Enum TypeOfValue
            [Nothing] = 0
            Number = 1
            [String] = 2
        End Enum

        Public Enum enumAccountStatus
            Active = 0
            Deactive = 1
        End Enum

        Public Enum enumAuthenticateResult
            InvalidAccount = 1
            ExpireAccount = 2
            Passed = 3
        End Enum

        Public Enum List
            Division = 1
            ItemCategory = 2
            ProductGroup = 3
            Brand = 4
            MenuGroup = 5
            VATGroup = 6
            UnitOfMeasure = 7
            SalesPriceType = 8
            StoreGroup = 9
            Feature = 10
            Province = 11
            District = 12
            GroupContent = 13
            Modules = 14
            UserGroup = 15
            UserDefine = 16
            Ward = 17
            TypeOfContent = 18
            Page = 19
            Zone = 20
        End Enum

        Public Enum typeOfColumn
            GetString = 1
            GetInt32 = 2
            GetInt64 = 3
            GetFloat = 4
            GetDateTime = 5
            GetDouble = 6
            GetBinary = 7
            GetBit = 8
            GetDecimal = 9
        End Enum

        Public Enum Result
            Successful = True
            [Error] = False
        End Enum

        Enum enumDBAccessType
            oledb = 1
            sql = 2
        End Enum

        Private Function ByteArrayToString(ByVal arrInput() As Byte) As String
            Dim i As Integer
            Dim sOutput As New StringBuilder(arrInput.Length)
            For i = 0 To arrInput.Length - 1
                sOutput.Append(arrInput(i).ToString("X2"))
            Next
            Return sOutput.ToString()
        End Function

        Public Function MD5Encrypt(ByVal flatString As String) As String
            Dim arrayHash As New UnicodeEncoding
            Dim arrayHashed As Byte()
            Dim md5hash As New MD5CryptoServiceProvider

            arrayHashed = md5hash.ComputeHash(arrayHash.GetBytes(flatString))

            Return ByteArrayToString(arrayHashed)
        End Function

        Public Function adjustNullString(ByVal stringValue As String) As String
            Return IIf(stringValue Is Nothing, String.Empty, stringValue)
        End Function

        Public Function adjustNullDB(ByVal DBValue As Object) As Object
            Return IIf(IsDBNull(DBValue), String.Empty, DBValue)
        End Function

        Public Function GetValue(ByVal dataReader As IDataReader, ByVal column As Integer, ByVal ColumnType As typeOfColumn) As Object
            Dim retValue As Object
            With dataReader
                If .IsDBNull(column) Then
                    Select Case ColumnType
                        Case typeOfColumn.GetString
                            retValue = ""
                        Case typeOfColumn.GetFloat
                            retValue = 0
                        Case typeOfColumn.GetInt32
                            retValue = 0
                        Case typeOfColumn.GetInt64
                            retValue = 0
                        Case typeOfColumn.GetDecimal
                            retValue = 0
                        Case typeOfColumn.GetBit
                            retValue = 0
                    End Select
                Else
                    Select Case ColumnType
                        Case typeOfColumn.GetString
                            retValue = .GetString(column)
                        Case typeOfColumn.GetFloat
                            retValue = .GetFloat(column)
                        Case typeOfColumn.GetInt32
                            retValue = .GetInt32(column)
                        Case typeOfColumn.GetInt64
                            retValue = .GetInt64(column)
                        Case typeOfColumn.GetDateTime
                            retValue = .GetDateTime(column)
                        Case typeOfColumn.GetDecimal
                            retValue = .GetDecimal(column)
                        Case typeOfColumn.GetBit
                            retValue = .GetBoolean(column)
                    End Select
                End If
            End With

            Return retValue
        End Function

        Public Function getNewSequence(ByVal TableName As String) As Integer
            Dim objDataReader As IDataReader
            Dim strSQL As String
            Dim iNewSequence As Integer = 1

            Try
                strSQL = "SELECT MAX([Sequence]) FROM " & TableName
                objDataReader = DBHelper.ExecuteReader(adv.Business.ConnectionString, CommandType.Text, strSQL)

                If objDataReader.Read() Then
                    iNewSequence = GetValue(objDataReader, 0, modGeneral.typeOfColumn.GetInt32) + 1
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not objDataReader Is Nothing Then
                    objDataReader.Close()
                End If
            End Try

            Return iNewSequence
        End Function

        Public Function subStringSafe(ByVal stringVal As String, ByVal startIndex As Integer, Optional ByVal length As Integer = 0) As String
            Dim retVal As String = String.Empty
            Try
                If length = 0 Then
                    retVal = stringVal.Substring(startIndex)
                Else
                    retVal = stringVal.Substring(startIndex, length)
                End If
            Catch ex As Exception
                Return retVal
            End Try
            Return retVal
        End Function


        Function Exists(ByVal OID As String, ByVal tableName As String) As Boolean
            Dim sqlStatement As String
            sqlStatement = "SELECT OID from [" & tableName & "] where OID = '" & OID & "'"
            OID = DBHelper.ExecuteScalar(adv.Business.ConnectionString, CommandType.Text, sqlStatement)
            Return Not (OID Is Nothing OrElse OID.Equals(String.Empty))
        End Function

        Public Function CreatKey(ByVal pTableName As String, ByVal pLenght As Integer, Optional ByVal pType As String = "") As String
            Dim strValue As String
            Dim arParams() As IDataParameter = New IDataParameter(2) {}

            arParams(0) = DBHelper.createParameter("@SoKhoa", SqlDbType.NVarChar, ParameterDirection.Output, 0)
            arParams(1) = DBHelper.createParameter("@TenBang", SqlDbType.NVarChar, ParameterDirection.Input, pTableName)
            arParams(2) = DBHelper.createParameter("@ChieuDai", SqlDbType.NVarChar, ParameterDirection.Input, 9)
            DBHelper.ExecuteNonQuery(adv.Business.ConnectionString, CommandType.StoredProcedure, "Sp_CapKhoa", arParams)
            strValue = arParams(0).Value.ToString
            Dim pPrefix As String
            pPrefix = "TT"
            Return pPrefix & Right("000000000000000" & strValue.Trim, 15 - pPrefix.Length)
        End Function
    End Module
End Namespace
