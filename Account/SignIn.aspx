<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="HEA.ePTW.Account.SignIn" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server" enableviewstate="false">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HEA Digital Safety Tools - Signin Page</title>
    <link rel="stylesheet" type="text/css" href="../Content/SignIn.css" />
    <script type="text/javascript" src="../Content/SignInRegister.js"></script>
</head>
<body>
  <div class="full-screen-container">
    <div class="login-container">
      <h1 class="login-title">HEA Digital Safety Tools (MTE)</h1>
      <form id="frmLogin" class="form" runat="server">
        <dx:ASPxFormLayout runat="server" ID="FormLayout" ClientInstanceName="formLayout" UseDefaultPaddings="False" ShowItemCaptionColon="False">
            <SettingsItemCaptions Location="Top" />
                <Items>
                    <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                        <Paddings PaddingLeft="2px" PaddingRight="5px" />
                        <Items>
                            <dx:LayoutItem Caption="User ID">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="UserNameTextBox" runat="server" Width="100%">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="User name is required" />
                                            </ValidationSettings>
                                            <ClientSideEvents Init="function(s, e){ s.Focus(); }" />
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                                <CaptionStyle ForeColor="White">
                                </CaptionStyle>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Password">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButtonEdit ID="PasswordButtonEdit" runat="server" Width="100%" Password="true" ClearButton-DisplayMode="Never">
                                            <ClearButton DisplayMode="Never"></ClearButton>
                                            <ButtonStyle Border-BorderWidth="0" Width="6" CssClass="eye-button" HoverStyle-BackColor="Transparent" PressedStyle-BackColor="Transparent">
                                                <PressedStyle BackColor="Transparent"></PressedStyle>
                                                <HoverStyle BackColor="Transparent"></HoverStyle>
                                                <Border BorderWidth="0px"></Border>
                                            </ButtonStyle>
                                            <ButtonTemplate>
                                                <div></div>
                                            </ButtonTemplate>
                                            <Buttons>
                                                <dx:EditButton>
                                                </dx:EditButton>
                                            </Buttons>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="Password is required" />
                                            </ValidationSettings>
                                            <ClientSideEvents ButtonClick="onPasswordButtonEditButtonClick" />
                                        </dx:ASPxButtonEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                                <CaptionStyle ForeColor="White">
                                </CaptionStyle>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxCaptcha ID="ASPxCaptchaePTW" runat="server" CharacterSet="123456789" ForeColor="White">
                                            <RefreshButtonStyle Font-Bold="False" ForeColor="White">
                                            </RefreshButtonStyle>
                                            <TextBoxStyle ForeColor="Black" />
                                            <ValidationSettings>
                                                <ErrorFrameStyle Font-Bold="True" ForeColor="#FF0066">
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <RefreshButton Position="Right" Text="">
                                            </RefreshButton>
                                            <TextBox LabelText="Type the code shown" Position="Top" />
                                            <ChallengeImage BackgroundColor="" Width="120" Height="45" BorderColor="" BorderWidth="1" ForegroundColor="Transparent">
                                            </ChallengeImage>
                                        </dx:ASPxCaptcha>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False" Name="GeneralError">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <div id="GeneralErrorDiv" runat="server" class="formLayout-generalErrorText"></div>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False" HorizontalAlign="Right">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton ID="SignInButton" runat="server" Width="130" Text="Log In" OnClick="SignInButton_Click"></dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                                <Paddings Padding="0px"></Paddings>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" />
        </dx:ASPxFormLayout>
      </form>
    </div>
  </div>
</body>
</html>
