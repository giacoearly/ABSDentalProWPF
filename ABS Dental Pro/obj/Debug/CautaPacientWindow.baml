   M S B A M L   `   `   `  ����I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  5   	   '    !ABS_Dental_Pro.CautaPacientWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ABS_Dental_Pro     5   	     � Title$  Caută pacient��5      $	��600��6   $
��1000��6*    � WindowStartupLocation$ CenterScreen=�67    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6r    � Icon$ /Images/search.png��6�    � 	Resources 5	         myLabels �� 
TargetType �� Property �� Value  myTextBoxes��%.  &	        &	 z     �� 5
   
   " ��6!   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��$ Bottom=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��
 �����6;   �� 5       � ��$ Bottom=�62   6C   5      5      .��5      �� .+�5   
   �� 	 �� Orientation$	 
Horizontal=�6   .+�5      �� -   5        
 ��Name$
 btnCauta��5       � ToolTip$ Caută pacient��6'    �� 	IsDefault . $��	45,20,0,0q�6g   $
��Left=�6z   $	��140��6�   $��40��6�   .��5      �� $	 
Horizontal=�6   .+�5      �� $��/Images/search.png��6   $
��Left=�68   $��Center=�5      $��0,0,30,0q�68    J� RenderTransformOrigin$ 0.5,0.5&�6J   $��35��6j   $��35��6u   5      �� 5      $��Caută��6    $��16(�6-   $��Center=�6;   $��Right=�6V   $��0q�6r   5      5      �� 5      -   5        $
 btnModifica��5      $ Modifică pacient��6*   $
��Left=�6E   $	��140��6`   $��40��6l   $��	50,20,0,0q�6x   {�.  .��5      �� $	 
Horizontal=�6   .+�5       �� $��/Images/edit.png��6   $��0,0,20,0q�66   $ 0.5,0.5&�6H   $��35��6h   $��35��6s   5!      �� 5"      $��	Modifică��6    $��16(�60   $��Center=�6>   $ 0.5,0.5&�6Y   5#      5$      �� 5%      -   5        $
 btnIstoricPacient��5%      $$ Afișează istoric programări��60   $
��Left=�6W   $	��140��6r   $��40��6~   $��	50,20,0,0q�6�   {�.  .��5&      �� $	 
Horizontal=�6   .+�5'      �� $!��/Images/History-icon128.png��6   $��0,0,20,0q�6A   $ 0.5,0.5&�6S   $��35��6s   5(      �� 5)      $��Istoric��6    $��16(�6/   $��Center=�6=   $ 0.5,0.5&�6X   $��0q�6x   5*      5+      �� 5,      -   5        $
 	btnSterge��5,      $ Șterge pacient��6(   $��Right=�6A   $	��140��6]   $��40��6i   $��	50,20,0,0q�6u   {�.  .��5-      �� $	 
Horizontal=�6   .+�5.      �� $��/Images/remove.png��6   $ 0.5,0.5&�68   $��0,0,25,0q�6X   $��35��6j   $��35��6u   5/      �� 50      $��Șterge��6    $��16(�6.   $��Center=�6<   6Y   51      52      �� 53      -   5        $
 
btnInchide��53      $ Închide fereastra��6)   $��Right=�6E   $	��140��6a   $��40��6m   $��	50,20,0,0q�6y    �� IsCancel . .��54      �� $	 
Horizontal=�6   .+�55      �� $��/Images/close.png��6   $ 0.5,0.5&�67   $��0,0,20,0q�6W   $��35��6i   $��35��6t   56      �� 57      $��Închide��6    $��16(�6/   $��Center=�6=   6Z   58      59      5:      �� 5;   
   -   5         ��Name$ tbCauta��5;      #��[ 6#   $��280,40,280,0q�6H   $��Center=�6^   $	��300��6y   �� 5<   
   #��[  6   $��Caută pacient după:��62   $��Center=�6P   $��0,20,0,0q�6m   6�   �� 5=   
   $	 
Horizontal=�6   $��90,0,0,0q�6.   .+�5>      	� -   5         	�Name$ rbNumarFisa��5>      $
��Left=�6/   $��16(�6J   $��220,10,40,0q�6X   .��6m   Număr Fișă6y   	� 5?      -   5        $ rbNumePrenume��5?      $
��Left=�61   $��16(�6L   $��	0,10,40,0q�6Z   $��Nume/Prenume��6m    d� 	IsChecked$
 TrueY�6�   	� 5@      -	   5        $ rbCnp��5@      $
��Left=�6)   $��16(�6D   $��0,10,0,0q�6R   .��6d   CNP6i   5A      &   System.Windows.Controls.DataGrid  5B   
   -
   5        $��dataGridPacienti��5B         SelectionMode$ Single=�6U      AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . ��. $
��Auto=�5E      $
��Auto=�5F      $	��300��6Q   $��0,20,0,0q�5G   j   $��14(�6|   ��5B   +   �� *listaPacientiProp+ � DataContext 5G      ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 5H      0  *System.Windows.Controls.DataGridTextColumn  5I      ,  &System.Windows.Controls.DataGridColumn   Header$ Număr Fișă��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	NumarFisa+  5J      $ Medic��6%    64   �� *Medic+  5K      $
 Nume��6%    63   �� *Nume+  5L      $ Prenume��6%    66   �� *	Prenume+  5M      $	 CNP��6%    62   �� *Cnp+  5N      $ Serie CI��6%    67   �� *	SerieCi+  5O      $ 	Număr CI��6%    67   �� *	NumarCi+  5P      $ Vârsta��6%    65   �� *Varsta+  5Q      $	 Sex��6%    62   �� *Sex+  5R      $ Telefon��6%    66   �� *	Telefon+  5S      $ Email��6%    64   �� *Email+  5T      $ Observații��6%    69   �� *
Observatii+5V      5W      5Z      