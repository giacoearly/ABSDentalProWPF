   M S B A M L   `   `   `  ���� I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  '    !ABS_Dental_Pro.CautaPacientWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:ABS_Dental_Pro       � Title$  Caută pacient��$	��600��$
��1000�� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � Icon$ /Images/search.png�� � 	Resources    myLabels �� 
TargetType �� Property �� Value  myTextBoxes��%�   &	        &	 =     �� " ��.���  � ��$ 16(���  � ��$ Bottom=��� " ��.���  � ��$ 16(���  � ��
 �������  � ��$ Bottom=�.���� .+��� 	 �� Orientation$	 
Horizontal=�.+��� -   
 ��Name$
 btnCauta�� � ToolTip$ Caută pacient�� �� 	IsDefault . $��	45,20,0,0q�$
��Left=�$	��140��$��40��.���� $	 
Horizontal=�.+��� $��/Images/search.png��$
��Left=�$��Center=�$��0,0,30,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��35��$��35���� $��Caută��$��16(�$��Center=�$��Right=�$��0q��� -   $
 btnModifica��$ Modifică pacient��$
��Left=�$	��140��$��40��$��	50,20,0,0q�{�.  .���� $	 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,20,0q�$ 0.5,0.5&�$��35��$��35���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� -   $
 btnIstoricPacient��$$ Afișează istoric programări��$
��Left=�$	��140��$��40��$��	50,20,0,0q�{�.  .���� $	 
Horizontal=�.+��� $!��/Images/History-icon128.png��$��0,0,20,0q�$ 0.5,0.5&�$��35���� $��Istoric��$��16(�$��Center=�$ 0.5,0.5&�$��0q��� -   $
 	btnSterge��$ Șterge pacient��$��Right=�$	��140��$��40��$��	50,20,0,0q�{�.  .���� $	 
Horizontal=�.+��� $��/Images/remove.png��$ 0.5,0.5&�$��0,0,25,0q�$��35��$��35���� $��Șterge��$��16(�$��Center=��� -   $
 
btnInchide��$ Închide fereastra��$��Right=�$	��140��$��40��$��	50,20,0,0q� �� IsCancel . .���� $	 
Horizontal=�.+��� $��/Images/close.png��$ 0.5,0.5&�$��0,0,20,0q�$��35��$��35���� $��Închide��$��16(�$��Center=��� -    ��Name$ tbCauta��#��[ $��280,40,280,0q�$��Center=�$	��300���� #��[  $��Caută pacient după:��$��Center=�$��0,20,0,0q��� $	 
Horizontal=�$��90,0,0,0q�.+�	� -    	�Name$ rbNumarFisa��$
��Left=�$��16(�$��220,10,40,0q�.��Număr Fișă	� -   $ rbNumePrenume��$
��Left=�$��16(�$��	0,10,40,0q�$��Nume/Prenume�� d� 	IsChecked$
 TrueY�	� -	   $ rbCnp��$
��Left=�$��16(�$��0,10,0,0q�.��CNP&   System.Windows.Controls.DataGrid  -
   $��dataGridPacienti��   SelectionMode$ Single=�   AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . ��. $
��Auto=�$
��Auto=�$	��300��$��0,20,0,0q�$��14(����� *listaPacientiProp+ � DataContext ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Număr Fișă��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	NumarFisa+  $ Medic�� �� *Medic+  $
 Nume�� �� *Nume+  $ Prenume�� �� *	Prenume+  $	 CNP�� �� *Cnp+  $ Serie CI�� �� *	SerieCi+  $ 	Număr CI�� �� *	NumarCi+  $ Vârsta�� �� *Varsta+  $	 Sex�� �� *Sex+  $ Telefon�� �� *	Telefon+  $ Email�� �� *Email+  $ Observații�� �� *
Observatii+