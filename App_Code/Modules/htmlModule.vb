Imports Microsoft.VisualBasic
Imports System.Data

Public Module htmlModule
    Public Function GetHeader() As String
        Return ""
    End Function

    Public Function GetMenuHome() As String
        Dim sSQL As String
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = 0 AND Published = 1 ORDER BY [Menu Order] "
        Dim tData As DataTable
        tData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Dim nIJ As Integer
        Dim sUrl As String = GetUrl()
        Dim sMenu As String = "", sCloseTag As String = ""
        Dim sKeyParent As String = ""
        For nIJ = 0 To tData.Rows.Count - 1
            If nIJ < 3 Then
                If nIJ = 0 Then
                    sMenu &= "<a class=""DivisionIcon"" style=""margin-left:14px;"" href=""#"" OnClick=""division_click('" & tData.Rows(nIJ).Item("No_") & "')"">"
                Else
                    sMenu &= "<a class=""DivisionIcon"" style=""margin-left:13px;"" href=""#"" OnClick=""division_click('" & tData.Rows(nIJ).Item("No_") & "')"">"
                End If

            Else
                If nIJ = 3 Then
                    sMenu &= "<a class=""DivisionIcon"" href=""#"" style=""margin-top : 20px;margin-left:14px;"" OnClick=""division_click('" & tData.Rows(nIJ).Item("No_") & "')"">"
                Else
                    sMenu &= "<a class=""DivisionIcon"" href=""#"" style=""margin-top : 20px;margin-left:13px;"" OnClick=""division_click('" & tData.Rows(nIJ).Item("No_") & "')"">"
                End If

            End If

            sMenu &= "<img alt=""" & tData.Rows(nIJ).Item("Name") & """ src=""Images/ProductGroup/" & tData.Rows(nIJ).Item("Images URL") & """ border=""0""/>"
            sMenu &= "</a>"
        Next
        Return sMenu
    End Function

    Public Function GetMenuVer2(ByVal sPageNo As String, Optional ByVal sCatNo As String = "") As String
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
        sMenu &= "<div class=""menunav"" style=""background-color:" & objPage.BackgroundColor & ";"">" & vbCrLf
        sMenu &= "<div id=""nav"" style=""float:left;width:900px;"">" & vbCrLf
        sMenu &= "<ul>" & vbCrLf
        For nIJ = 0 To tData.Rows.Count - 1
            sMenu &= "<li>" & vbCrLf

            sTmp = "style=""margin-left :0px;"

            If sCatNo = tData.Rows(nIJ).Item("No_") Then
                sTmp &= "background-color:" & tData.Rows(nIJ).Item("Background Color") & ";border-bottom:1px solid " & tData.Rows(nIJ).Item("Background Color") & ";"
            End If
            sTmp &= """"
            sMenu &= "<span tabindex=""1"" " & sTmp & ">" & vbCrLf
            sMenu &= tData.Rows(nIJ).Item("Name").ToString.ToUpper
            sMenu &= "</span>" & vbCrLf

            sMenu &= "<ul style=""margin-left:-" & (nIJ * 110) + nIJ & "px;"">" & vbCrLf

            sMenu &= "<div >" & vbCrLf
            sMenu &= GetMenuByDiv(tData.Rows(nIJ).Item("No_"))

            sMenu &= "</div>" & vbCrLf
            sMenu &= "</ul>" & vbCrLf
            sMenu &= "</li>" & vbCrLf
        Next
        sMenu &= "</ul>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        sMenu &= "<div style=""float:right;width:100px;"">" & vbCrLf
        sMenu &= "<a href=""" & GetUrl() & "tin-tuc/""> "
        sMenu &= "<div class=""newsbutton"">TIN CÔNG NGHỆ</div>"
        sMenu &= "</a>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        Return sMenu
    End Function

    Function GetDivisionFromCategory(ByVal sCategoryNo As String) As String
        Dim SQL As String
        SQL = "SELECT [Parent No_] FROM [Good Menu] WHERE [Link Display] = '" & sCategoryNo & "'"
        Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
    End Function


    Function GetMenuByDiv(ByVal sDivisionNo As String) As String
        Dim SQL As String, nIJ As Integer, sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Dim sProducUrl As String = sUrl
        Dim pTbl As DataTable
        Dim objGmn As New adv.Business.GoodMenu
        objGmn.Load(sDivisionNo)
        sProducUrl &= IIf(objGmn.ParentLink.Trim <> "", objGmn.ParentLink & "/", "") & objGmn.LinkDisplay & "/"
        SQL = " SELECT * FROM [Good Menu] WHERE [Parent No_] = '" & sDivisionNo & "' AND Published = 1 ORDER BY [Menu Order] "

        pTbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For nIJ = 0 To pTbl.Rows.Count - 1
            If nIJ < 5 Then
                sHtml &= "<div class=""CategoryIcon"" style=""margin-top:13px;background:" & objGmn.BackgroundColor & ";"">"
            Else
                sHtml &= "<div class=""CategoryIcon"" style=""margin-top:13px;background:" & objGmn.BackgroundColor & ";"">"
            End If

            sHtml &= "<div>" & "<a href=""" & sProducUrl & pTbl.Rows(nIJ).Item("Link Display") & """>" & "<img alt=""" & pTbl.Rows(nIJ).Item("Name") & """ src=""" & sUrl & "Images/ProductGroup/" & pTbl.Rows(nIJ).Item("Images URL") & """ border=""0""/></div>"
            sHtml &= "<div style=""padding-left:3px;""><a href=""" & sProducUrl & pTbl.Rows(nIJ).Item("Link Display") & "/"">"
            sHtml &= pTbl.Rows(nIJ).Item("Name")
            sHtml &= "</a></div>"
            sHtml &= "</div>"
        Next

        Return sHtml
    End Function

    Function GetPathWayByDiv(ByVal sCatNo As String) As String
        Dim SQL As String, nIJ As Integer, sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Dim pTbl As DataTable
        Dim sDivisionNo As String = ""
        Dim sCategoryLink As String = ""
        Dim objGmn As New adv.Business.GoodMenu

        objGmn.LoadFromCat(sCatNo)
        sDivisionNo = objGmn.No_

        sCategoryLink = sUrl & objGmn.ParentLink & "/" & objGmn.LinkDisplay & "/"
        SQL = " SELECT * FROM [Good Menu] WHERE [Parent No_] = '" & sDivisionNo & "' AND Published = 1 ORDER BY [Menu Order] "

        sHtml &= "<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">"
        sHtml &= "<tr>"
        sHtml &= "<td style=""background:" & objGmn.BackgroundColor & ";"">"
        pTbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For nIJ = 0 To pTbl.Rows.Count - 1

            If sCatNo = pTbl.Rows(nIJ).Item("No_") Then
                sHtml &= "<a href=""" & sCategoryLink & pTbl.Rows(nIJ).Item("Link Display") & "/"" class=""LinkCat"" style=""background:#fff;color:#000;"">"
            Else
                sHtml &= "<a href=""" & sCategoryLink & pTbl.Rows(nIJ).Item("Link Display") & "/"" class=""LinkCat"">"

            End If
            sHtml &= pTbl.Rows(nIJ).Item("Name")
            sHtml &= "</a>"
        Next
        sHtml &= "</td>"
        sHtml &= "</tr>"
        sHtml &= "</table>"
        Return sHtml
    End Function

    Public Function ShowBanner(ByVal sPositionNo As String, ByVal ShowStyle As Integer, Optional ByVal sCategory As String = "", Optional ByVal sPage As String = "") As String
        Dim sSQL As String
        Dim sToday As String
        Dim sHtml As String = "", sUrl As String = ""
        Dim sClosedTag As String = ""

        sToday = Date2Char(getToday)
        
        Dim sWhere As String = ""
        If sCategory.Trim <> "" Then
            sWhere &= " AND [Good Menu No_] = '" & sCategory & "'"
        End If

        sWhere &= " AND ([Page No_] = '" & sPage & "' OR ISNULL([Page No_],'') = '') "

        sUrl = GetUrl()
        sSQL = " SELECT * FROM Banner WHERE [Starting Date] <= '" & sToday & "' AND (RTRIM([Ending Date]) >= '" & sToday & "' OR RTRIM([Ending Date]) = '') AND Show = 1 " & _
                " AND [Position No_] = '" & sPositionNo & "'" & sWhere & _
                " ORDER BY [Order Position] "
        Dim tDt As DataTable
        tDt = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        If tDt.Rows.Count = 0 Then Return ""
        Dim nIJ As Integer
        If tDt.Rows.Count > 1 Then
            Select Case ShowStyle
                Case 1
                    Dim sLink As String = "", sNum As String = ""
                    sHtml &= "<div class=""slider-wrapper theme-default"">" & vbCrLf
                    sHtml &= "        <div id=""slider"" class=""nivoSlider"">" & vbCrLf
                    For nIJ = 0 To tDt.Rows.Count - 1
                        If tDt.Rows(nIJ).Item("Url").ToString.Trim <> "" Then
                            sHtml &= "<a href=""" & sUrl & "banner/" & tDt.Rows(nIJ).Item("No_") & """ " & IIf(tDt.Rows(nIJ).Item("New Windows") = 1, " target=""_blank"" ", "") & ">"
                            sHtml &= "<img src=""" & GetImgUrl() & "Images/Banners/" & tDt.Rows(nIJ).Item("Images Src") & """/></a>" & vbCrLf
                        Else
                            sHtml &= "<img src=""" & GetImgUrl() & "Images/Banners/" & tDt.Rows(nIJ).Item("Images Src") & """/>" & vbCrLf
                        End If
                    Next
                    sHtml &= "    </div>" & vbCrLf
                    sHtml &= "    </div>" & vbCrLf
                Case 2
                    For nIJ = 0 To tDt.Rows.Count - 1
                        sClosedTag = ""
                        sHtml &= IIf(sHtml.Trim = "", "<div style=""padding-top:0px;"">", "<div style=""padding-top:3px;"">") & vbCrLf
                        If tDt.Rows(nIJ).Item("Url").ToString.Trim <> "" Then
                            sHtml &= "<a href=""" & sUrl & "banner/" & tDt.Rows(nIJ).Item("No_") & """ " & IIf(tDt.Rows(nIJ).Item("New Windows") = 1, " target=""_blank"" ", "") & ">" & vbCrLf
                            sClosedTag = "</a>"
                        End If
                        sHtml &= "<img src=""" & GetImgUrl() & "Images/Banners/" & tDt.Rows(nIJ).Item("Images Src") & """ width=""100%""/>" & vbCrLf
                        sHtml &= sClosedTag & vbCrLf
                        sHtml &= "</div>" & vbCrLf
                    Next
                Case 3
                    sHtml = "<ul id=""hotproduct"" class=""jcarousel-skin-ie7"">"
                    For nIJ = 0 To tDt.Rows.Count - 1
                        sClosedTag = ""
                        sHtml &= "<li>"
                        If tDt.Rows(nIJ).Item("Url").ToString.Trim <> "" Then
                            sHtml &= "<a href=""" & sUrl & "banner/" & tDt.Rows(nIJ).Item("No_") & """ " & IIf(tDt.Rows(nIJ).Item("New Windows") = 1, " target=""_blank"" ", "") & ">" & vbCrLf
                            sClosedTag = "</a>"
                        End If
                        sHtml &= "<img src=""" & GetImgUrl() & "Images/Banners/" & tDt.Rows(nIJ).Item("Images Src") & """/>" & vbCrLf
                        sHtml &= sClosedTag & vbCrLf
                        sHtml &= "</li>" & vbCrLf
                    Next
                    sHtml &= "</ul>" & vbCrLf
                Case 4
                    sHtml = "<ul id=""brandlogo"" class=""jcarousel-skin-tango"">"
                    For nIJ = 0 To tDt.Rows.Count - 1
                        sClosedTag = ""
                        sHtml &= "<li>"
                        If tDt.Rows(nIJ).Item("Url").ToString.Trim <> "" Then
                            sHtml &= "<a href=""" & sUrl & "banner/" & tDt.Rows(nIJ).Item("No_") & """ " & IIf(tDt.Rows(nIJ).Item("New Windows") = 1, " target=""_blank"" ", "") & ">" & vbCrLf
                            sClosedTag = "</a>"
                        End If
                        sHtml &= "<img src=""" & GetImgUrl() & "Images/Banners/" & tDt.Rows(nIJ).Item("Images Src") & """ width=""" & tDt.Rows(nIJ).Item("Width") & """/>" & vbCrLf
                        sHtml &= sClosedTag & vbCrLf
                        sHtml &= "</li>" & vbCrLf
                    Next
                    sHtml &= "</ul>" & vbCrLf
                Case 5
                    Dim objModules As New adv.Business.Modules
                    Dim sWidth As String = ""
                    objModules.LoadByPosition(sPositionNo)
                   
                    For nIJ = 0 To tDt.Rows.Count - 1
                        sClosedTag = ""
                        If objModules.CssClass.Trim <> "" Then
                            sHtml &= "<div class=""" & objModules.CssClass & """>"
                        Else
                            sHtml &= "<div style=""float:left;margin-right:10px;"">"
                        End If
                        If tDt.Rows(nIJ).Item("Url").ToString.Trim <> "" Then
                            sHtml &= "<a href=""" & sUrl & "banner/" & tDt.Rows(nIJ).Item("No_") & """ " & IIf(tDt.Rows(nIJ).Item("New Windows") = 1, " target=""_blank"" ", "") & ">" & vbCrLf
                            sClosedTag = "</a>"
                        End If
                        If sWidth.Trim = "" Then
                            sWidth = tDt.Rows(nIJ).Item("Width")
                        End If
                        sHtml &= "<img src=""" & GetImgUrl() & "Images/Banners/" & tDt.Rows(nIJ).Item("Images Src") & """ />" & vbCrLf
                        sHtml &= sClosedTag & vbCrLf
                        sHtml &= "</div>" & vbCrLf
                    Next

            End Select
        Else
            If ShowStyle Then
                Dim objModules As New adv.Business.Modules
                Dim sWidth As String = ""
                objModules.LoadByPosition(sPositionNo)
                If objModules.MaxWidth = 1 Then
                    sWidth = " width = ""100%"" "
                End If
                If objModules.CssClass.Trim <> "" Then
                    sHtml = "<div class=""" & objModules.CssClass & """>"
                Else
                    sHtml = "<div>"
                End If
                sClosedTag = ""

                If tDt.Rows(nIJ).Item("Url").ToString.Trim <> "" Then
                    sHtml &= "<a href=""" & sUrl & "banner/" & tDt.Rows(nIJ).Item("No_") & """ " & IIf(tDt.Rows(nIJ).Item("New Windows") = 1, " target=""_blank"" ", "") & ">" & vbCrLf
                    sClosedTag = "</a>"
                End If
                If sWidth.Trim = "" Then
                    sWidth = IIf(tDt.Rows(nIJ).Item("Width") <> "0", " width=""" & tDt.Rows(nIJ).Item("Width") & """", "")
                End If
                sHtml &= "<img src=""" & GetImgUrl() & "Images/Banners/" & tDt.Rows(nIJ).Item("Images Src") & """" & sWidth & """/>" & vbCrLf
                sHtml &= sClosedTag & vbCrLf
                sHtml &= "</div>" & vbCrLf
            End If
        End If
        Return sHtml
    End Function

    Public Function ShowCart() As String
        Dim sHtml As String = ""
        Return sHtml
    End Function


    Public Function ShowLinkMenu(ByVal sModulesNo_ As String, Optional ByVal sCssClass As String = "") As String
        Dim pData As DataTable
        Dim objLnk As New adv.Business.LinkMenu
        Dim objM As New adv.Business.ModulesOfSite
        Dim sHtml As String, nIJ As Integer
        Dim sCss As String = "", sTarget As String = ""
        pData = objLnk.GetMenu(" [Group No_]  = '" & sModulesNo_ & "' ")
        objM.Load(sModulesNo_)
        sHtml = ""
        Select Case objM.Align
            Case 0
                For nIJ = 0 To pData.Rows.Count - 1
                    If pData.Rows(nIJ).Item("Link Type").ToString.Trim = 1 Then
                        sTarget = " target=""_blank"" "
                    End If
                    sHtml &= "<div> <a href=""" & pData.Rows(nIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """" & sTarget & " >" & pData.Rows(nIJ).Item("Name") & "</a></div>" & vbCrLf
                Next
            Case 1, 2
                For nIJ = 0 To pData.Rows.Count - 1
                    If pData.Rows(nIJ).Item("Link Type").ToString.Trim = 1 Then
                        sTarget = " target=""_blank"" "
                    End If
                    sHtml &= IIf(sHtml.Trim <> "", "&nbsp;" & objM.SeparateBar & "&nbsp;", "") & "<a href=""" & pData.Rows(nIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """ " & IIf(sCssClass.Trim <> "", "class=""" & sCssClass & """", "") & sTarget & " >" & pData.Rows(nIJ).Item("Name") & "</a>"
                Next
            Case 3
                sHtml = "<ul>" & vbCrLf
                For nIJ = 0 To pData.Rows.Count - 1
                    If pData.Rows(nIJ).Item("Link Type").ToString.Trim = 1 Then
                        sTarget = " target=""_blank"" "
                    End If
                    sHtml &= "<li> <a href=""" & pData.Rows(nIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """ " & IIf(sCssClass.Trim <> "", "class=""" & sCssClass & """", "") & sTarget & ">" & pData.Rows(nIJ).Item("Name") & "</a></li>" & vbCrLf
                Next
                sHtml &= "<ul>" & vbCrLf
            Case 4
                For nIJ = 0 To pData.Rows.Count - 1
                    sCss = pData.Rows(nIJ).Item("Class CSS")
                    If sCss.Trim = "" Then sCss = sCssClass
                    If pData.Rows(nIJ).Item("Link Type").ToString.Trim = 1 Then
                        sTarget = " target=""_blank"" "
                    End If

                    sHtml &= "<div style=""margin-top:5px;""> <a href=""" & pData.Rows(nIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """ " & IIf(sCss.Trim <> "", "class=""" & sCss & """", "") & sTarget & ">" & pData.Rows(nIJ).Item("Name") & "</a></div>" & vbCrLf
                Next
        End Select
        Return sHtml
    End Function

    Public Function ShowHeader() As String
        Dim sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Return sHtml
    End Function

    Public Function GetFullLinkCategory(ByVal sCatNo As String) As String
        Dim objGmn As New adv.Business.GoodMenu
        Dim sHtml As String = ""
        objGmn.Load(sCatNo)
        sHtml = objGmn.LinkDisplay
        objGmn.Load(objGmn.ParentNo_)
        sHtml = IIf(objGmn.ParentLink.Trim <> "", objGmn.ParentLink & "/", "") & objGmn.LinkDisplay & "/" & sHtml & "/"
        Return sHtml
    End Function

    Public Function ShowBannerMetro(ByVal sPositionNo As String, ByVal sColor As String, ByVal sClassLink As String) As String
        Dim SQL As String
        Dim sToday As String
        Dim sHtml As String = "", sUrl As String = ""
        Dim sClosedTag As String = ""
        sToday = Date2Char(getToday)
        sUrl = GetUrl()
        Dim tDt As DataTable
        SQL = "SELECT Top 1 * FROM Banner WHERE [Starting Date] <= '" & sToday & "' AND (RTRIM([Ending Date]) >= '" & sToday & "' OR RTRIM([Ending Date]) = '') AND Show = 1 " & _
                " AND [Position No_] = '" & sPositionNo & "'" & _
                " ORDER BY [Order Position] "
        tDt = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tDt.Rows.Count = 0 Then Return ""
        sHtml = ""
        sHtml &= " <div> " & vbCrLf
        sHtml &= IIf(tDt.Rows(0).Item("Url").ToString.Trim = "", " <a href=""#"">", " <a href=""" & tDt.Rows(0).Item("Url").ToString.Trim & """>") & vbCrLf
        sHtml &= "<img class=""full"" src=""" & GetUrl() & "Images/Banners/" & tDt.Rows(0).Item("Images Src") & """/>" & vbCrLf
        sHtml &= "</a>" & vbCrLf
        sHtml &= "<span class=""tile-title"">" & tDt.Rows(0).Item("Name") & "</span>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= " <div style=""background-color:" & sColor & ";"">" & vbCrLf
        sHtml &= IIf(tDt.Rows(0).Item("Url").ToString.Trim = "", "<a class=""" & sClassLink & """ href=""#"">", "<a class=""" & sClassLink & """ href=""" & tDt.Rows(0).Item("Url").ToString.Trim & """>") & vbCrLf
        sHtml &= tDt.Rows(0).Item("Name") & vbCrLf
        sHtml &= IIf(tDt.Rows(0).Item("Url").ToString.Trim = "", "", "</a>") & vbCrLf
        sHtml &= "</div>"
        Return sHtml

    End Function

    Function SharedOnline(ByVal sUrl As String) As String
        Dim sHtml As String = ""

        sHtml &= "<div id=""widgetaddthis"">"
        sHtml &= "<div class=""addthis_toolbox addthis_default_style"">"
        sHtml &= "<a class=""addthis_button_preferred_1 addthis_button_facebook at300b"" title=""Facebook"" href=""#"">"
        sHtml &= "<span class=""at16nc at300bs at15nc at15t_facebook at16t_facebook"">"
        sHtml &= "<span class=""at_a11y"">Share on facebook</span></span>"
        sHtml &= "</a>"
        sHtml &= "<a class=""addthis_button_preferred_2 addthis_button_twitter at300b"" title=""Tweet This"" href=""#"">"
        sHtml &= "<span class=""at16nc at300bs at15nc at15t_twitter at16t_twitter"">"
        sHtml &= "<span class=""at_a11y"">Share on twitter</span></span>"
        sHtml &= "</a>"
        sHtml &= "<a class=""addthis_button_compact at300m"" href=""#"">"
        sHtml &= "<span class=""at16nc at300bs at15nc at15t_compact at16t_compact"">"
        sHtml &= "<span class=""at_a11y"">More Sharing Services</span></span></a>"
        sHtml &= "<span class=""fb-like"" data-href=""" & sUrl & """ data-send=""false"" data-layout=""button_count"" data-width=""450"" data-show-faces=""true""></span>"
        sHtml &= "<div class=""atclear""></div>"
        sHtml &= "</div>"
        sHtml &= "<script type=""text/javascript"">"
        sHtml &= "var addthis_config = { ""data_track_clickback"": true };"
        sHtml &= "</script>"
        sHtml &= "<script src=""http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4dd4d6ee3d619e4d"" type=""text/javascript"">"
        sHtml &= "</script>"

        sHtml &= "</div>"

        Return sHtml
    End Function

End Module


