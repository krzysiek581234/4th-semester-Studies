   M S B A M L   `   `   `  ����A  =lab4v2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:lab4v2lab4v2  5   	       lab4v2.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:lab4v2     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��569* �6   �� 5      $
��180* �6   �� 5      $	��17* �6   �� 5      $	��34* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridView1��5      $
��Left=�5      $	��355��6.   $��	10,0,0,10q�6;   $��Bottom=�6N   $	��559��6i      AutoGenerateColumns$
 True��5   *   ��6E   ��  � DataContext 6]   �� 6u   c� 5   
   $
��Left=�6   $��30��6-   $	��Top=�69   $	��761��6Q   $��3��6]   .@�5      �� $��25��6   $��0q�6    $	��Top=�6+   $��80��6C   $��Search for:��6N   �� 5      -   5        $��searchTextBox��5   3   $��0,2,0,1q�6   $	��120��6'   6J   �� 5      $��
Search in:��6   $��25��6)   $	��Top=�65   �� 5      -   5        $��comboBox��5   R   $��25��6   $��0q�6#   $	��Top=�6.   $	��120��6F   6R   �� 5      -   5        $��Search��5      $��25��6&   $	��Top=�62   $��75��6J   6U   5      �� 5   
   -   5         ��Name$ Model��5      $��
Sort Model��6    $��1��65   $��Center=�6E   $	��Top=�6b   $	��120��6z   $��25��6�   $��0,15,0,0q�6�   6�   �� 5   
   -   5        $
 Year��5      $��	Sort Year��6   $��1��63   $��Center=�6C   $	��Top=�6`   $	��120��6x   $��25��6�   $��0,52,0,0q�6�   6�   �� 5   
   -   5        $ Motor��5      $��
Sort Motor��6    $��1��65   $��Center=�6E   $	��Top=�6b   $	��120��6z   $��25��6�   $��0,91,0,0q�6�   6�   �� 5   
   $��1��6   $	��Top=�6%   $��
30,130,0,0q�6=   $	��200��6Q   .+�5       �� $��Model:��6   �� 5!      -   5         ��Name$ xModel��5!      �� 5"      $��EngineModel:��6   �� 5#      -	   5        $ xEngineModel��5#      �� 5$      $��Horsepower:��6   �� 5%      -
   5        $ xHorsepower��5%      �� 5&      $��Displacement:��6   �� 5'      -   5        $ xDisplacement��5'      �� 5(      $��Year:��6   �� 5)      -   5        $ xYear��5)      �� 5*      -   5        $	��Add��5*      $��0,10,0,0q�6#   65   5+      5.      50      