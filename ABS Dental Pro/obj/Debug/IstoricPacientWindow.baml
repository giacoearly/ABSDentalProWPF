   M S B A M L   `   `   `  ����I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  5   	   )    #ABS_Dental_Pro.IstoricPacientWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ABS_Dental_Pro     5   	     � Title$  Istoric programări��5      $	��500��6#    � WindowStartupLocation$ CenterScreen=�60    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6k    � Icon$  /Images/History-icon32.png��5	      $	��700��62    � 	Resources 5
         myLabels �� 
TargetType �� Property �� Value  myTextBoxes��%.  &	        &	 z     �� 5   
   " ��6!   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��$ Bottom=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��
 �����6;   �� 5       � ��$ Bottom=�62   6C   5      5      .��5      �� .+�5   
   �� 	 �� Orientation$	 
Horizontal=�6   .+�5      �� -   5        
 ��Name$
 btnModifica��5      $
��Left=�6+   $	��140��6F   $��40��6R   $��
170,20,0,0q�6^   {�.   � ToolTip$ Modifică descriere��5   /   .��5      �� $	 
Horizontal=�6   .+�5      �� $��/Images/edit.png��6   $��0,0,20,0q�66    J� RenderTransformOrigin$ 0.5,0.5&�6H   $��35��6h   $��35��6s   5      �� 5      $��	Modifică��6    $��16(�60   $��Center=�6>   $ 0.5,0.5&�6Y   5      5      �� 5      -   5        $
 
btnInchide��5      $��Right=�6)   $	��140��6E   $��40��6Q   $��	50,20,0,0q�6]    �� IsCancel . .��5       �� $	 
Horizontal=�6   .+�5!      �� $��/Images/close.png��6   $ 0.5,0.5&�67   $��0,0,20,0q�6W   $��35��6i   $��35��6t   5"      �� 5#      $��Închide��6    $��16(�6/   $��Center=�6=   6Z   5$      5%      5&      &   System.Windows.Controls.DataGrid  5'   
   -   5        $��dataGridIstorii��5'         SelectionMode$ Single=�6S      AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . ��. $
��Auto=�5*      $
��Auto=�5+      $	��360��6Q   $��0,20,0,0q�5,   j   $��14(�6|   ��5'   *   �� *listaIstoriiProp+ � DataContext 5,      ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ Medic��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *Medic+  5/      $
 Data��6%    63   �� *Data+  50      $ 	Descriere��6%    68   �� *	Descriere+52      53      56      