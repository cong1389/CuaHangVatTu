Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class Userdefine
#Region "Khai báo"
        Private mUsername As String = ""
        Private mPassword As String = ""
        Private mStaffNo_ As String = ""
        Private mUserGroupNo_ As String = ""
        Private mEmail As String = ""
        Private mActive As Integer = 0
        Private mFullname As String = ""
        Private mStatus As Integer = 0
        Private mLastLogon As String = ""
        Private mDivision As String = ""

        Property Username() As String
            Get
                Return mUsername
            End Get
            Set(ByVal value As String)
                mUsername = value
            End Set
        End Property
        Property Password() As String
            Get
                Return mPassword
            End Get
            Set(ByVal value As String)
                mPassword = value
            End Set
        End Property
        Property StaffNo_() As String
            Get
                Return mStaffNo_
            End Get
            Set(ByVal value As String)
                mStaffNo_ = value
            End Set
        End Property
        Property UserGroupNo_() As String
            Get
                Return mUserGroupNo_
            End Get
            Set(ByVal value As String)
                mUserGroupNo_ = value
            End Set
        End Property
        Property Email() As String
            Get
                Return mEmail
            End Get
            Set(ByVal value As String)
                mEmail = value
            End Set
        End Property
        Property Active() As Integer
            Get
                Return mActive
            End Get
            Set(ByVal value As Integer)
                mActive = value
            End Set
        End Property
        Property Fullname() As String
            Get
                Return mFullname
            End Get
            Set(ByVal value As String)
                mFullname = value
            End Set
        End Property
        Property Status() As Integer
            Get
                Return mStatus
            End Get
            Set(ByVal value As Integer)
                mStatus = value
            End Set
        End Property
        Property LastLogon() As String
            Get
                Return mLastLogon
            End Get
            Set(ByVal value As String)
                mLastLogon = value
            End Set
        End Property
        Property Division() As String
            Get
                Return mDivision
            End Get
            Set(ByVal value As String)
                mDivision = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sUserName As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [User Define] WHERE Username = '" & sUserName & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mUsername = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mPassword = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mStaffNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mUserGroupNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mEmail = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mActive = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mFullname = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mStatus = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mLastLogon = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mDivision = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    SqlReader.Close()
                    Return True
                Else
                    SqlReader.Close()
                    Return False
                End If
            Catch ex As Exception

                Return False
            End Try
        End Function

        Public Function Save(ByVal sUserNameOld As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(10) {}
            Try
                arParams(0) = DBHelper.createParameter("@Username", SqlDbType.NVarChar, ParameterDirection.Input, mUsername)
                arParams(1) = DBHelper.createParameter("@Password", SqlDbType.NVarChar, ParameterDirection.Input, mPassword)
                arParams(2) = DBHelper.createParameter("@StaffNo_", SqlDbType.NVarChar, ParameterDirection.Input, mStaffNo_)
                arParams(3) = DBHelper.createParameter("@UserGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mUserGroupNo_)
                arParams(4) = DBHelper.createParameter("@Email", SqlDbType.NVarChar, ParameterDirection.Input, mEmail)
                arParams(5) = DBHelper.createParameter("@Active", SqlDbType.Int, ParameterDirection.Input, mActive)
                arParams(6) = DBHelper.createParameter("@Fullname", SqlDbType.NVarChar, ParameterDirection.Input, mFullname)
                arParams(7) = DBHelper.createParameter("@Status", SqlDbType.Int, ParameterDirection.Input, mStatus)
                arParams(8) = DBHelper.createParameter("@LastLogon", SqlDbType.NVarChar, ParameterDirection.Input, mLastLogon)
                arParams(9) = DBHelper.createParameter("@Division", SqlDbType.NVarChar, ParameterDirection.Input, mDivision)
                arParams(10) = DBHelper.createParameter("@UserNameOld", SqlDbType.NVarChar, ParameterDirection.Input, sUserNameOld)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "W_UserDefine", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetList(Optional ByVal sCondition As String = "") As DataTable
            Dim SQL As String
            SQL = " SELECT Fullname, Username, G.[Group Name], U.Email, U.[Last Logon], CONVERT(Bit,U.Active) Active" & _
                    " FROM [User Define] U INNER JOIN [User Group] G ON U.[User Group No_] = G.No_ "
            If sCondition.Trim <> "" Then
                SQL &= sCondition
            End If

            SQL &= " ORDER BY 1 "
            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        End Function

        Function ChangePwd(ByVal sUserName As String, ByVal sNewPwd As String) As Boolean
            Try
                Dim SQL As String
                SQL = " UPDATE [User Define] SET [Password] = '" & sNewPwd & "' WHERE Username = '" & sUserName & "'"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function


#End Region

    End Class
End Namespace