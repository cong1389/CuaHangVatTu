Imports Microsoft.VisualBasic
Imports System.Data

Public Module advTheme

    Function GettblMenu(ByVal sLevel As Integer, Optional ByVal sConditions As String = "") As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        If sConditions.Trim <> "" Then sWhere = " AND " & sConditions
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = " & sLevel & " AND Published = 1 AND [Using For] = 0 " & sWhere & " ORDER BY [Menu Order] "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Function ShowAdvMenu() As String
        Dim sHtml As String = ""
        Dim tbl As DataTable
        tbl = GettblMenu(0)
        Dim nIJ As Integer
        Dim sUrl As String = GetUrl()
        sHtml &= "          <div id=""custommenu"" class=""force"" stype="""">" & vbCrLf
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<div id=""menu" & tbl.Rows(nIJ).Item("No_") & """ class=""menu act top1"" onmouseover=""wppShowMenuPopup(this, 'popup" & tbl.Rows(nIJ).Item("No_") & "');"" onmouseout=""wppHideMenuPopup(this, event, &#39;popup" & tbl.Rows(nIJ).Item("No_") & "&#39;, &#39;menu" & tbl.Rows(nIJ).Item("No_") & "&#39;)"">" & vbCrLf
            sHtml &= "  <div class=""parentMenu"">" & vbCrLf
            sHtml &= "      <a href=""" & sUrl & "sp/" & tbl.Rows(nIJ).Item("Link Display") & "/"">" & vbCrLf
            sHtml &= "      <span>" & tbl.Rows(nIJ).Item("Name") & "</span>" & vbCrLf
            sHtml &= "      </a>" & vbCrLf
            sHtml &= "  </div>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= GetSubAdvMenu(tbl.Rows(nIJ).Item("No_"))
            sHtml &= "       <div class=""clearBoth""></div>" & vbCrLf
        Next
        sHtml &= "          </div>" & vbCrLf


        Return sHtml
    End Function

    Function GetSubAdvMenu(ByVal sParentNo As String) As String
        Dim sHtml As String = ""
        Dim nIJ As Integer
        Dim sClass As String = ""
        Dim tbl As DataTable
        tbl = GettblMenu(1, " [Parent No_] = '" & sParentNo & "'")
        Dim objGmn As New adv.Business.GoodMenu
        objGmn.Load(sParentNo)
        Dim sProductUrl As String = ""
        Dim sProductGroupMn As String = ""
        sProductUrl = GetUrl() & "sp/"
        sHtml &= "<div id=""popup" & objGmn.No_ & """ class=""wp-custom-menu-popup"" onmouseout=""wppHideMenuPopup(this, event, &#39;popup" & objGmn.No_ & "&#39;, &#39;menu" & objGmn.No_ & "&#39;)"">" & vbCrLf
        sHtml &= "  <div class=""block1"">" & vbCrLf
        sHtml &= "      <div class=""full_block"" style=""width:780px"">"
        Dim kIJ As Integer = 0
        Dim nNum As Integer = 0
        Dim nNumInCollumn As Integer = 14
        Dim nNumCols As Integer = 0
        For nIJ = 0 To tbl.Rows.Count - 1
            sProductGroupMn = GetMenuProductGroup(tbl.Rows(nIJ).Item("No_"), kIJ)
            If (nNum + 1 + kIJ) >= nNumInCollumn Then
                nNum = 0
                nNumCols += 1
            End If
            If nNum = 0 Then
                If nNumCols <> 0 Then sHtml &= "</div>"
                sHtml &= "      <div class=""column"">" & vbCrLf
            End If
            sHtml &= "<a class=""itemMenuName level1"" href=""" & sProductUrl & tbl.Rows(nIJ).Item("Link Display") & "/""><span>" & tbl.Rows(nIJ).Item("Name") & "</span></a>"
            sHtml &= sProductGroupMn
            nNum += 1
            nNum += kIJ
        Next
        If tbl.Rows.Count > 0 Then sHtml &= "          </div>" & vbCrLf
        sHtml &= "      </div>" & vbCrLf
        sHtml &= "  </div>" & vbCrLf
        sHtml &= "  <div class=""block2"">" & vbCrLf
        If objGmn.ImagesURL.Trim <> "" Then
            sHtml &= "<img class=""pic"" src=""" & GetUrl() & "Images/ProductGroup/" & objGmn.ImagesURL & """>"
        End If
        sHtml &= "  </div>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        Return sHtml
    End Function

    Function GetMenuProductGroup(ByVal sMenuCategory As String, ByRef nNumOfRows As Integer) As String
        Dim tbl As DataTable
        tbl = GettblMenu(2, " [Parent No_] = '" & sMenuCategory & "'")
        Dim objGmn As New adv.Business.GoodMenu
        objGmn.Load(sMenuCategory)
        Dim sProductUrl As String = GetUrl() & "sp/"
        Dim sHtml As String = ""
        nNumOfRows = tbl.Rows.Count
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<a class=""itemMenuName level2"" href=""" & sProductUrl & tbl.Rows(nIJ).Item("Link Display") & "/""><span>" & tbl.Rows(nIJ).Item("Name") & "</span></a>"
        Next
        Return sHtml
    End Function

    Public Function GetMenuVer3(ByVal sPageNo As String, Optional ByVal sCatNo As String = "") As String
        Dim sSQL As String
        Dim objPage As New adv.Business.Page
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = 0 AND Published = 1 AND [Using For] = 0 AND [Page No_] = '" & sPageNo & "' ORDER BY [Menu Order] "
        Dim tData As DataTable
        tData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Dim nIJ As Integer
        Dim sUrl As String = GetUrl()
        Dim sMenu As String = "", sCloseTag As String = ""
        Dim sKeyParent As String = ""
        Dim sTmp As String = ""
        objPage.Load(sPageNo)
        sMenu &= "<div class = ""window"">" & vbCrLf
        sMenu &= "<div id=""nav"" style=""background-color:" & objPage.BackgroundColor & ";width:750px;"">" & vbCrLf
        sMenu &= "<ul>" & vbCrLf

        For nIJ = 0 To tData.Rows.Count - 1
            sMenu &= "<li>" & vbCrLf
            If nIJ = 0 Then
                sTmp = "style=""margin-left :0px;"
            Else
                sTmp = "style=""margin-left :0px;"
            End If
            If sCatNo = tData.Rows(nIJ).Item("No_") Then
                sTmp &= "background-color:" & tData.Rows(nIJ).Item("Background Color") & ";color:#000;border-bottom:1px solid " & tData.Rows(nIJ).Item("Background Color") & ";"""
            Else
                sTmp &= """"
            End If

            sMenu &= "<span tabindex=""1"" " & sTmp & ">" & vbCrLf
            sMenu &= "<img src=""" & GetUrl() & "/Images/Template/" & tData.Rows(nIJ).Item("No_") & ".png""/>"
            sMenu &= "</span>" & vbCrLf
            If nIJ > 5 Then
                sMenu &= "<ul style=""margin-left:-290px;"">" & vbCrLf
            Else
                sMenu &= "<ul>" & vbCrLf
            End If

            sMenu &= "<div >" & vbCrLf
            sMenu &= GetMenuByDivV3(tData.Rows(nIJ).Item("No_"))

            sMenu &= "</div>" & vbCrLf
            sMenu &= "</ul>" & vbCrLf
            sMenu &= "</li>" & vbCrLf
        Next
        sMenu &= "</ul>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        sMenu &= "<div style=""float:right;width:250px;height:50px;background-color:#FFE061;"">" & vbCrLf
        sMenu &= "<div class=""menubutton"" style=""border-right: 1px solid #FFE061;"">"
        sMenu &= "<a href=""" & GetUrl() & "apl/khuyen-mai/""> "
        sMenu &= "<img src=""" & GetUrl() & "Images/Template/km.png"" border=""0""/>"
        sMenu &= "</a>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        sMenu &= "<div class=""menubutton"">"
        sMenu &= "<a href=""" & GetUrl() & "tu-van-tieu-dung/""> "
        sMenu &= "<img src=""" & GetUrl() & "Images/Template/tttv.png"" border=""0""/>"
        sMenu &= "</a>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        sMenu &= "</div>" & vbCrLf

        sMenu &= "</div>" & vbCrLf
        Return sMenu
    End Function

    Function GetMenuByDivV3(ByVal sDivisionNo As String) As String
        Dim SQL As String, nIJ As Integer, sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Dim sProducUrl As String = sUrl
        Dim pTbl As DataTable
        Dim objGmn As New adv.Business.GoodMenu
        objGmn.Load(sDivisionNo)
        sProducUrl &= IIf(objGmn.ParentLink.Trim <> "", objGmn.ParentLink & "/", "") & objGmn.LinkDisplay & "/"
        SQL = " SELECT * FROM [Good Menu] WHERE [Parent No_] = '" & sDivisionNo & "' AND Published = 1 ORDER BY [Menu Order] "

        pTbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim kIJ As Integer = 1
        sHtml &= "<div class=""submenu"">"
        sHtml &= "<div class=""block1"">"
        For nIJ = 0 To pTbl.Rows.Count - 1
            If kIJ = 1 Then
                sHtml &= "<div class=""menucol1"">"
            End If
            sHtml &= "<div style=""padding-left:3px;""><a href=""" & sProducUrl & pTbl.Rows(nIJ).Item("Link Display") & "/"">"
            sHtml &= pTbl.Rows(nIJ).Item("Name")
            sHtml &= "</a></div>"
            kIJ += 1
            If kIJ = 11 Then
                sHtml &= "</div>"
                kIJ = 1
            End If
        Next
        If kIJ < 11 And kIJ > 1 Then sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "<div class=""block2"">"
        sHtml &= "<img src=""" & GetUrl() & "Images/ProductGroup/" & sDivisionNo & ".png"">"
        sHtml &= "</div>"
        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowLinkFooter() As String
        Dim SQL As String = ""
        Dim sHtml As String = ""
        SQL = "SELECT * FROM [Link Menu] WHERE [Position No_] = '20' AND [Is Group] = 1 ORDER BY [Order Position] "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        Dim kIJ As Integer = 0
        Dim nNum As Integer = 0
        Dim nNumInCollumn As Integer = 8
        Dim nNumCols As Integer = 0
        Dim sSubLink As String
        For nIJ = 0 To tbl.Rows.Count - 1
            sSubLink = ShowLinkOfGroup(tbl.Rows(nIJ).Item("No_"), kIJ)
            If (nNum + 1 + kIJ) >= nNumInCollumn Then
                nNum = 0
                nNumCols += 1
            End If
            If nNum = 0 Then
                If nNumCols <> 0 Then sHtml &= "</div>"
                sHtml &= "      <div class=""column"">" & vbCrLf
            End If
            If tbl.Rows(nIJ).Item("URL Link").ToString.Trim <> "" Then
                sHtml &= "<div " & IIf(tbl.Rows(nIJ).Item("Class CSS").ToString.Trim <> "", " Class =""" & tbl.Rows(nIJ).Item("Class CSS").ToString & """", "") & " ><a  href=""" & tbl.Rows(nIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """ > " & tbl.Rows(nIJ).Item("Name") & "</a></div>"
            Else
                sHtml &= "<div " & IIf(tbl.Rows(nIJ).Item("Class CSS").ToString.Trim <> "", " Class =""" & tbl.Rows(nIJ).Item("Class CSS").ToString & """", "") & " >" & tbl.Rows(nIJ).Item("Name") & "</div>"
            End If
            sHtml &= sSubLink
            nNum += 1
            nNum += kIJ
        Next
        sHtml &= "</div>"

        Return sHtml

    End Function

    Function ShowLinkOfGroup(ByVal sGroup As String, ByRef nNumOfItem As Integer) As String
        Dim SQL As String = ""
        Dim sHtml As String = ""
        SQL = "SELECT * FROM [Link Menu] WHERE [Position No_] = '20' AND [Is Group] = 0 AND [Parent No_] = '" & sGroup & "' ORDER BY [Order Position] "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim NIJ As Integer
        nNumOfItem = tbl.Rows.Count - 1
        For NIJ = 0 To tbl.Rows.Count - 1
            If tbl.Rows(NIJ).Item("Class CSS").ToString.Trim <> "" Then
                sHtml &= "<div class=""" & tbl.Rows(NIJ).Item("Class CSS").ToString.Trim & """>"
            Else
                sHtml &= "<div>"
            End If
            If tbl.Rows(NIJ).Item("Link Type") = 1 Then
                sHtml &= "<a href=""" & tbl.Rows(NIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """ target=""_blank"">"
            Else
                sHtml &= "<a href=""" & tbl.Rows(NIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """ >"
            End If
            sHtml &= tbl.Rows(NIJ).Item("Name")
            sHtml &= "</a >"
            sHtml &= "</div>"
        Next
        Return sHtml
    End Function
End Module
