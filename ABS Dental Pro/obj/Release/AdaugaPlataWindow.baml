   M S B A M L   `   `   `  ���� I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  &     ABS_Dental_Pro.AdaugaPlataWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:ABS_Dental_Pro       � Title$"  Adaugă plată lucrare nouă�� � Icon$ Images/add32.png��$	��410��$	��500�� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=� � 	Resources    myLabels �� 
TargetType �� Property �� Value  myTextBoxes  myComboBoxes  myTextBlocks  myTextBlocks2  styleCalendar&   System.Windows.Controls.Calendar	 �� 
TargetType5  /System.Windows.Controls.Primitives.CalendarItem
  Name��%�  &	        &	 =     &	 �     &	 �     &	 �     &	 �     �� " ��.���  � ��$ 16(���  � ��$ Bottom=��� " ��.���  � ��$ 16(���  � ��
 �������  � ��$ Bottom=��� " ��.���  � ��$ 16(��� " ��.���  � ��$ 18(��� " ��.���  � ��$ 16(��� "  .���  � �� �� "	  .R�(� $	��300��$	��300��.��  $
 PART_CalendarItem��#��z��#��z��#��z��.���� $��10q�.+��� -    ��Name$ 
tbSituatie��$��Pacient��$��20(�$��	20,0,0,20q�� $��1,0,-1,0q� � ColumnDefinitions �� $	��150 ���  � RowDefinitions �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��70 �.+��� $��Medic��$��0��#��[ $��0���� -    ��Name$ cbMedic��$��1��$��0��#��[ $��0q�$��28��$	��253��$
��Left=�$	��Top=��� $��Total de plătit��$��0��#��[ $��1���� -    ��Name$ tbTotal��$��1��#��[ $��Center=�$	��Top=�$��0q�$��80��$
��Left=�$��1���� $��Tranșă plătită acum��$��2��$��0��#��[ �� -   $ tbTransa��$��2��$��1��#��[ $	��Top=�$��80��$
��Left=�$��Center=��� $��Rest de plătit��$��3��$��0��#��[ �� -   $ tbRest��
���  ��$
��Bold"�$��3��$��1��#��[ $	��Top=�$��80��$
��Left=��� $
��Data��$��4��$��0��#��[ �� $��	Descriere��$��5��$��0��#��[ �� -   $ tbDescriere��$��5��$��1��#��[ $	��Top=�$
��Left=�$	��300��$��Center=�$��1.062,0,0,0q��� -    ��Name$ btnAdaugaPlata�� � ToolTip$" Adaugă plată lucrare nouă��$��6��$��0��$��2��$��30,0,0,0q�$	��140��$��40��$��Center=�$��Center=�.����  �� Orientation$ 
Horizontal=�.+��� $��Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q�(  "System.Windows.Controls.DatePicker  -     Name$ 
datePicker��   CalendarStyle# [ $
��Left=�$	��120��$��0q�$��4��$��1��$��25��$	��Top=�