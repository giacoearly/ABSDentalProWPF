   M S B A M L   `   `   `  ����I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  5   	   /    )ABS_Dental_Pro.SelecteazaProgramareWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ABS_Dental_Pro     5   	     � Title$  Selectează programare��5   	    � Icon$ Images/select.png��6'   $	��300��6@   $	��300��6M    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6o    � Topmost .  � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5	         myLabels �� 
TargetType �� Property	 �� Value  myTextBoxes  myComboBoxes  myRadioButtons��%�  &	        &	 z     &	     &	 g    �� 5
   
   " ��6!   .�5      ��  � ��$	 16(�6)   66   �� 5       � ��$	 Bottom=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$	 16(�6)   66   �� 5       � ��
	 �����6;   �� 5       � ��$	 Bottom=�62   6C   5      �� 5   
   " ��6&   .�5      ��  � ��$	 16(�6)   66   5      �� 5   
   " 	�6(   .�5      ��  � ��$	 16(�6)   66   5      5      .��5      � -   5        
 �Name$

 grid��5       � ColumnDefinitions 5   
   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $	��27* �6   �� 5       $	��27* �6   �� 5!      $	��27* �6   �� 5"      $	��27* �6   �� 5#      $	��27* �6   �� 5$      $	��27* �6   �� 5%      $	��27* �6   �� 5&      $	��80* �6   .+�5(   
   	� -   5         	�Name$ rbProgramare1��5(      $��1��6-    	� 	GroupName$ 
Programare��6:   $��
Programare��6Q   #��[ 6f   $��Center=�6�   $��95,0,0,0q�6�   $��Center=�6�   $
��Left=�6�   $y�Hidden=�6  	� 5)   
   -   5        $ rbProgramare2��5)      $��2��6-   $ 
Programare��6:   $��
Programare��6Q   #��[ 6f   $��Center=�6�   $��95,0,0,0q�6�   $
��Left=�6�   $��Center=�6�   $y�Hidden=�6  	� 5*   
   -   5        $ rbProgramare3��5*      $��3��6-   $ 
Programare��6:   $��
Programare��6Q   #��[ 6f   $��Center=�6�   $��95,0,0,0q�6�   $
��Left=�6�   $��Center=�6�   $y�Hidden=�6  	� 5+   
   -   5        $ rbProgramare4��5+      $��4��6-   $ 
Programare��6:   $��
Programare��6Q   #��[ 6f   $��Center=�6�   $��95,0,0,0q�6�   $
��Left=�6�   $��Center=�6�   $y�Hidden=�6  	� 5,   
   -   5        $ rbProgramare5��5,      $��5��6-   $ 
Programare��6:   $��
Programare��6Q   #��[ 6f   $��Center=�6�   $��95,0,0,0q�6�   $
��Left=�6�   $��Center=�6�   $y�Hidden=�6  �� 5-   
   -   5         ��Name$ btnSelecteaza��5-      $��7��6*   $
��75,0q�67   $��35��6E   .��5.      ��  �� Orientation$ 
Horizontal=�6   .+�5/      �� $��/Images/select128.png��6   $
��Left=�67   $��Center=�50      $��0,0,20,0q�68    J� RenderTransformOrigin$ 0.5,0.5&�6J   $��25��6j   $��25��6u   51      �� 52      $��Selectează��6   $��16(�6.   $��Center=�6<   $��Right=�6W   $��0q�6s   53      54      55      56      