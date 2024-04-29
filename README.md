# MeterKnife Third **3**

- 本项目是一个开源软件项目。
- 本项目开源协议是《木兰宽松许可证, 第2版》
- 本项目开发语言：C#

## 关于 MeterKnife Third
  0. MeterKnife Third 将定义为一个自动化工业采集平台，仪器仪表采集将是其重要的一部份，但会更加深入的面向各种通讯手段的数据采集，例如485总线、Can总线、MBUS总线、RTU/DTU、Socket主从、IVI.Visa、自定义设备等等……。作者希望MeterKnife平台能为仪器仪表、传感器、自控集成、以及最终用户提供实时的物联网数据采集平台、生产运行监控平台、实时数据存储平台。
  1. MeterKnife Third在Gitee与Github上保持开源，主要实现面向多仪器多通道多传感器的触发、采集与数据存储、图表显示、分析；
  2. MeterKnife Third将遵循由常用到全部的迭代原则实现SCPI标准语法和命令的可视化快速使用；
  3. MeterKnife Third将实现仪器仪表常用的GPIB,TCPIP,串口,USB通讯手段的单独与复合使用；
  4. MeterKnife Third采用.Net6做为开发框架，界面采用.NET MAUI做为界面层框架；
  5. MeterKnife Third将逐步支持NI PXI系统；
  6. 定义为2024年10月完成Beta版研发；

## 开发历史

### 2024-4-29

- 忙于工作，又停滞了一年多的时间，这些时间又积累了一些开发思路；
- 近期开始从0开始研发，会采用一些新的技术；
- 所有代码移到历史代码分支，如有朋友感兴趣可以去history分支查看；

### 2023-1-29

- 启动新的研发进程，与其他工作同步进行。
- 新的研发进程的思路是：
  - PC端应用程序框架做为独立的平台，在另外的项目中进行研发，nuget引入；
  - 本项目只进行核心的数据采集研发；
  - 以Cli模式进行初期研发，以强制ViewModel与前端彻底脱耦；
  - 界面全面采用WPF技术；

### 2021-9-3

- 开发停滞了一年多的时间，近期有一些时间了，继续工作。力争这次能够完成研发工作。初步打算投入1-2个月的业余时间进行。
- 去年和一些朋友合伙创业，现在看来再次无果而终。那么就端正心态，休养一下，准备再次奋斗。
- (为尝试Git变基，增加一些改动)

### 2020-2-22

- 工作也较忙，利用挤出来的业余时间开发，前端界面的设计也大体成型了；
- 剩下的工作简单计划一下：
  - 测量动作管理：1天；
  - 图表优化：2天；
  - 接驳器管理：1天；
  - 仪器管理：1天；
  - 工程管理：1天；
  - 被测物管理：1天；
  - 软件全局优化，选项设置等：1天；
  - 软件安装，版本控制：1天；
  - 云端服务：2天； 

### 2020-2-17
- 后端的多采集服务基本运行正常；进入前端界面开发（已进行2-3天）。

### 2020-2-11
- 在一个设计上十分的纠结，接收到IMeasureService的测量结果后，谁来管理存储呢？总觉得放在AppManager里太高了。

### 2020-2-10
- 2020年的春节是一个铭刻历史的春节，宅在家中，做了一些工作。至今天，后端数据的采集与存储已完成，并较好的实现了最初的设想，即：不限端口；不限被测物；不限仪器数量；
- 近几日开始界面的开发；
- 武汉加油！湖北加油！中国加油！天佑中华！

### 2020-1-14
- 历时5年了，一直忙于工作，久久没有再更新软件，在群里见非常多的朋友都在使用这个软件，非常高兴，但也是非常遗憾，有相当多的功能没有实现，并且还留下来了不少设计缺陷和Bug。
- 近几日，将时间规划了一下，决定今年认真升级MeterKnife平台。

### 2015.11.28
- 以压缩包方式发布上位机程序，MKL.v1.0.beta3.zip。
- 基本功能已经实现。

### 2015.7.15
- 发布上位机程序，MeterKnife Lite 0.9，程序绿色打包为“MKL.v0.9.alpha1.zip”，见群共享。

### 2015.5.1 
- 原采集系统需求定义较复杂，但开发时间需要较多，精简出Lite版本需求进行开发。

### 2014.12.1 
- 与MeterCare硬件团队共同启动开发仪表采集系统。