   M S B A M L   `   `   `  ���� I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  )    #ABS_Dental_Pro.IstoricPacientWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:ABS_Dental_Pro       � Title$  Istoric programări��$	��500�� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � Icon$  /Images/History-icon32.png��$	��700�� � 	Resources    myLabels �� 
TargetType �� Property �� Value  myTextBoxes��%�   &	        &	 =     �� " ��.���  � ��$ 16(���  � ��$ Bottom=��� " ��.���  � ��$ 16(���  � ��
 �������  � ��$ Bottom=�.���� .+��� 	 �� Orientation$	 
Horizontal=�.+��� -   
 ��Name$
 btnModifica��$
��Left=�$	��140��$��40��$��
170,20,0,0q�{�.   � ToolTip$ Modifică descriere��.���� $	 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,20,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��35��$��35���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� -   $
 
btnInchide��$��Right=�$	��140��$��40��$��	50,20,0,0q� �� IsCancel . .���� $	 
Horizontal=�.+��� $��/Images/close.png��$ 0.5,0.5&�$��0,0,20,0q�$��35��$��35���� $��Închide��$��16(�$��Center=�&   System.Windows.Controls.DataGrid  -   $��dataGridIstorii��   SelectionMode$ Single=�   AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . ��. $
��Auto=�$
��Auto=�$	��360��$��0,20,0,0q�$��14(����� *listaIstoriiProp+ � DataContext ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Medic��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Medic+  $
 Data�� �� *Data+  $ 	Descriere�� �� *	Descriere+