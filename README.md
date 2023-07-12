# SDRSharp.SDT: Satellite Doppler Tracker / SDRSharp人造卫星多普勒追踪插件

## 介绍 / Introduction

SDT（Satellite Doppler Tracker / 人造卫星多普勒追踪器）是一个SDRSharp插件，这款插件使用了[SGP.NET](https://github.com/parzivail/SGP.NET)包进行多普勒效应计算，并能预测卫星过境，获取卫星的频率信息，自动计算多普勒位移，并控制SDRSharp的频率。

SDT (Satellite Doppler Tracker) is an SDRSharp plugin that utilizes the [SGP.NET](https://github.com/parzivail/SGP.NET) package to calculate the Doppler effect. It can predict satellite transits, obtain satellite frequency information, automatically calculate Doppler shifts, and control the frequency of SDRSharp.

## 特性 / Features

- **预测卫星过境 / Predict satellite transits**: SDT可以预测选定的人造卫星的过境时间。/ SDT can predict the transit times of the selected artificial satellite.
- **读取卫星频率信息 / Read satellite frequency information**: SDT可以读取并显示选定的人造卫星的详细频率信息。/ SDT can read and display the detailed frequency information of the selected artificial satellite.
- **自动计算多普勒位移 / Automatic calculation of Doppler shift**: SDT可以计算出多普勒位移，并自动调整SDRSharp的频率。/ SDT can calculate the Doppler shift and adjust the frequency of SDRSharp accordingly.


## 安装 / Installation

1. 从本页面下载最新版本的SDT的压缩文件SDT.zip。 / Download the latest version of the SDT compressed file SDT.zip from this page.
2. 解压下载的文件，将其中的SDT文件夹复制到SDRSharp的plugins目录。 / Unzip the downloaded file and copy the SDT folder into the plugins directory of SDRSharp.
3. 启动SDRSharp，打开“插件管理”窗口，然后启用SDT插件。 / Start SDRSharp, open the "Plugin Manager" window, and then enable the SDT plugin.

## 使用方法 / Usage

1. 启动SDRSharp并加载SDT插件。 / Start SDRSharp and load the SDT plugin.
2. 在SDT的界面中设置经纬度和角度。 / Set the latitude, longitude, and angle in the SDT interface.
3. 点击"Satellites"按钮选取你想要追踪的人造卫星。 / Click the "Satellites" button to select the artificial satellite you want to track.
4. 点击“预测卫星”可以显示选定卫星的频率信息。 / Clicking "Predict Satellite" can display the frequency information of the selected satellite.
5. 点击频率信息，SDT将自动计算多普勒位移并调整SDRSharp的频率。 / Click the frequency information, and SDT will automatically calculate the Doppler shift and adjust the frequency of SDRSharp.
6. 点击"Start"按钮开始追踪。 / Click the "Start" button to start tracking.
7. "Update"按钮用于从网络更新卫星的TLE和信息。/ The "Update" button is used to update the TLE and information of the satellite from the network.

## 许可证 / License

本项目采用MIT许可证。详情请查看[LICENSE](LICENSE.txt)文件。 / This project is licensed under the MIT License. For more details, please check the [LICENSE](LICENSE.txt) file.
