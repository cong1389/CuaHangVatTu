Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class WishListLine
#Region "Khai báo"
        Private mDocumentNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mItemNo_ As String = ""
        Private mVariants As String = ""
        Private mDescriptions As String = ""
        Private mCustomerNo_ As String = ""
        Private mPageNo_ As String = ""
        Private mHomeoneGuestNo_ As String = ""

        Property DocumentNo_() As String
            Get
                Return mDocumentNo_
            End Get
            Set(ByVal value As String)
                mDocumentNo_ = value
            End Set
        End Property
        Property LineNo_() As Integer
            Get
                Return mLineNo_
            End Get
            Set(ByVal value As Integer)
                mLineNo_ = value
            End Set
        End Property
        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
            End Set
        End Property
        Property Variants() As String
            Get
                Return mVariants
            End Get
            Set(ByVal value As String)
                mVariants = value
            End Set
        End Property
        Property Descriptions() As String
            Get
                Return mDescriptions
            End Get
            Set(ByVal value As String)
                mDescriptions = value
            End Set
        End Property
        Property CustomerNo_() As String
            Get
                Return mCustomerNo_
            End Get
            Set(ByVal value As String)
                mCustomerNo_ = value
            End Set
        End Property
        Property PageNo_() As String
            Get
                Return mPageNo_
            End Get
            Set(ByVal value As String)
                mPageNo_ = value
            End Set
        End Property
        Property HomeoneGuestNo_() As String
            Get
                Return mHomeoneGuestNo_
            End Get
            Set(ByVal value As String)
                mHomeoneGuestNo_ = value
            End Set
        End Property

#End Region
#Region "Function"

        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(7) {}
            Try
                arParams(0) = DBHelper.createParameter("@DocumentNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDocumentNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(3) = DBHelper.createParameter("@Variants", SqlDbType.NVarChar, ParameterDirection.Input, mVariants)
                arParams(4) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                arParams(5) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                arParams(6) = DBHelper.createParameter("@PageNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPageNo_)
                arParams(7) = DBHelper.createParameter("@HomeoneGuestNo_", SqlDbType.NVarChar, ParameterDirection.Input, mHomeoneGuestNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_WishlistLine", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function ExistsItemInWishList(ByVal sWishListNo As String, ByVal sItemNo As String) As Boolean
            Dim sQL As String
            sQL = "SELECT [Item No_] FROM [Wishlist Line] WHERE [Document No_] = '" & sWishListNo & "' AND [Item No_] = '" & sItemNo & "'"
            Dim sTmp As String
            sTmp = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, sQL), "").ToString.Trim
            Return sTmp <> ""
        End Function
        Function GetNumberIteam(ByVal sWishListNo As String) As Integer
            Dim sQL As String
            sQL = "SELECT [Item No_] FROM [Wishlist Line] WHERE [Document No_] = '" & sWishListNo & "'"
            Dim sTmp As DataTable
            sTmp = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sQL).Tables(0)
            Return sTmp.Rows.Count
        End Function
#End Region

    End Class
End Namespace
