   M S B A M L   `   `   `  ���� I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  ,    &ABS_Dental_Pro.CautaPacientPlataWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:ABS_Dental_Pro       � Title$  Caută pacient��$	��480��$
��1000�� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � Icon$ /Images/search.png�� � 	Resources    myLabels �� 
TargetType �� Property �� Value  myTextBoxes��%�   &	        &	 =     �� " ��.���  � ��$ 16(���  � ��$ Bottom=��� " ��.���  � ��$ 16(���  � ��
 �������  � ��$ Bottom=�.���� .+��� 	 �� Orientation$	 
Horizontal=�$��2.124,0,-2.124,0q�.+��� -   
 ��Name$
 btnCauta�� � ToolTip$$ Caută pacient pentru o plată�� �� 	IsDefault . $��	45,20,0,0q�$
��Left=�$	��140��$��40��.���� $	 
Horizontal=�.+��� $��/Images/search.png��$
��Left=�$��Center=�$��0,0,30,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��35��$��35���� $��Caută��$��16(�$��Center=�$��Right=�$��0q��� -   $
 
btnIncarca��$ Încarcă date pacient�� . $��	45,20,0,0q�$
��Left=�$	��140��$��40��{�.  .���� $	 
Horizontal=�.+��� $#��/Images/load2-upload-icon.png��$
��Left=�$	��Top=�$��0,0,30,0q�$ 0.5,0.5&�$��35��$��35���� $��	Încarcă��$��16(�$��Center=�$��Right=�$��0q��� -   $
 
btnInchide��$ Închide fereastra��$��Right=�$	��140��$��40��$��	50,20,0,0q� �� IsCancel . .���� $	 
Horizontal=�.+��� $��/Images/close.png��$ 0.5,0.5&�$��0,0,20,0q�$��35��$��35���� $��Închide��$��16(�$��Center=��� $��280,20,280,0q�$	 
Horizontal=�.+��� $��Nume/Prenume:��$��16(��� -    ��Name$ tbCauta��#��[ $��Center=�$	��200��$��10,0,0,0q��� $	 
Horizontal=�&   System.Windows.Controls.DataGrid  -   $��dataGridPacienti��   SelectionMode$ Single=�   AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . ��. $
��Auto=�$
��Auto=�$	��300��$��0,20,0,0q�$��14(����� *listaPacientiProp+ � DataContext ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Număr Fișă��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	NumarFisa+  $ Medic�� �� *Medic+  $
 Nume�� �� *Nume+  $ Prenume�� �� *	Prenume+  $	 CNP�� �� *Cnp+  $ Serie CI�� �� *	SerieCi+  $ 	Număr CI�� �� *	NumarCi+  $ Vârsta�� �� *Varsta+  $	 Sex�� �� *Sex+  $ Telefon�� �� *	Telefon+  $ Email�� �� *Email+  $ Observații�� �� *
Observatii+