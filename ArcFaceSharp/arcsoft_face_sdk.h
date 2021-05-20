/*******************************************************************************
* Copyright(c) ArcSoft, All right reserved.
*
* This file is ArcSoft's property. It contains ArcSoft's trade secret, proprietary
* and confidential information.
*
* DO NOT DISTRIBUTE, DO NOT DUPLICATE OR TRANSMIT IN ANY FORM WITHOUT PROPER
* AUTHORIZATION.
*
* If you are not an intended recipient of this file, you must not copy,
* distribute, modify, or take any action in reliance on it.
*
* If you have received this file in error, please immediately notify ArcSoft and
* permanently delete the original and any copy of any file and any printout
* thereof.
*********************************************************************************/

#ifndef _ARCSOFT_SDK_ASF_H_
#define _ARCSOFT_SDK_ASF_H_

#include "amcomdef.h"
#include "asvloffscreen.h"

#ifdef __cplusplus
extern "C" {
#endif

#define ASF_NONE				0x00000000	//无属性
#define ASF_FACE_DETECT			0x00000001	//此处detect可以是tracking或者detection两个引擎之一，具体的选择由detect mode 确定
#define ASF_FACERECOGNITION		0x00000004	//人脸特征
#define ASF_AGE					0x00000008	//年龄
#define ASF_GENDER				0x00000010	//性别
#define ASF_FACE3DANGLE			0x00000020	//3D角度
#define ASF_FACELANDMARK		0x00000040	//额头区域检测
#define ASF_LIVENESS			0x00000080	//RGB活体
#define ASF_IMAGEQUALITY		0x00000200	//图像质量检测
#define ASF_IR_LIVENESS			0x00000400	//IR活体
#define ASF_FACESHELTER			0x00000800  //人脸遮挡
#define ASF_MASKDETECT			0x00001000	//口罩检测
#define ASF_UPDATE_FACEDATA		0x00002000	//人脸信息


#define ASF_MAX_DETECTFACENUM   10          //该版本最大支持同时检测10张人脸

	//检测模式
	enum ASF_DetectMode{
		ASF_DETECT_MODE_VIDEO = 0x00000000,		//Video模式，一般用于多帧连续检测
		ASF_DETECT_MODE_IMAGE = 0xFFFFFFFF		//Image模式，一般用于静态图的单次检测
	};


	//检测时候人脸角度的优先级，在文档中初始化接口中有图示说明，请参考
	enum ASF_OrientPriority {
		ASF_OP_0_ONLY = 0x1,		// 常规预览下正方向
		ASF_OP_90_ONLY = 0x2,		// 基于0°逆时针旋转90°的方向
		ASF_OP_270_ONLY = 0x3,		// 基于0°逆时针旋转270°的方向
		ASF_OP_180_ONLY = 0x4,		// 基于0°旋转180°的方向（逆时针、顺时针效果一样）
		ASF_OP_ALL_OUT = 0x5		// 全角度
	};

	//orientation 角度，逆时针方向
	enum ASF_OrientCode {
		ASF_OC_0 = 0x1,			// 0 degree 
		ASF_OC_90 = 0x2,		// 90 degree 
		ASF_OC_270 = 0x3,		// 270 degree 
		ASF_OC_180 = 0x4,   	// 180 degree 
		ASF_OC_30 = 0x5,		// 30 degree 
		ASF_OC_60 = 0x6,		// 60 degree 
		ASF_OC_120 = 0x7,		// 120 degree 
		ASF_OC_150 = 0x8,		// 150 degree 
		ASF_OC_210 = 0x9,		// 210 degree 
		ASF_OC_240 = 0xa,		// 240 degree 
		ASF_OC_300 = 0xb,		// 300 degree 
		ASF_OC_330 = 0xc		// 330 degree 
	};

	//检测模型
	enum ASF_DetectModel {
		ASF_DETECT_MODEL_RGB = 0x1	//RGB图像检测模型
		//预留扩展其他检测模型
	};

	//人脸比对可选的模型
	enum ASF_CompareModel{
		ASF_LIFE_PHOTO = 0x1,	//用于生活照之间的特征比对，推荐阈值0.80
		ASF_ID_PHOTO = 0x2		//用于证件照或生活照与证件照之间的特征比对，推荐阈值0.82
	};

	enum ASF_RegisterOrNot{
		ASF_RECOGNITION = 0x0,  //用于识别照人脸特征提取
		ASF_REGISTER = 0x1      //用于注册照人脸特征提取
	};

	//版本信息
	typedef struct {
		MPChar Version;				// 版本号
		MPChar BuildDate;			// 构建日期
		MPChar CopyRight;			// Copyright
	}ASF_VERSION, *LPASF_VERSION;

	//图像数据
	typedef LPASVLOFFSCREEN LPASF_ImageData;

	//人脸信息
	typedef struct{
		MPVoid		data;		// 人脸信息
		MInt32		dataSize;	// 人脸信息长度
	} ASF_FaceDataInfo, *LPASF_FaceDataInfo;

	//单人脸信息
	typedef struct {
		MRECT		faceRect;		        // 人脸框信息
		MInt32		faceOrient;		        // 输入图像的角度，可以参考 ArcFaceCompare_OrientCode
		ASF_FaceDataInfo faceDataInfo;		// 单张人脸信息
	} ASF_SingleFaceInfo, *LPASF_SingleFaceInfo;

	//多人脸信息
	typedef struct {
		MRECT* 		faceRect;			        // 人脸框信息
		MInt32*		faceOrient;			        // 输入图像的角度，可以参考 ArcFaceCompare_OrientCode .
		MInt32		faceNum;			        // 检测到的人脸个数
		MInt32*     faceID;				        // face ID，IMAGE模式下不返回FaceID
		MFloat*		wearGlasses;		        // 戴眼镜置信度[0-1],推荐阈值0.5
		MInt32*		leftEyeClosed;	            // 左眼状态 0 未闭眼；1 闭眼
		MInt32*		rightEyeClosed;	            // 右眼状态 0 未闭眼；1 闭眼
		MInt32*	    faceShelter;	            // "1" 表示 遮挡, "0" 表示  未遮挡, "-1" 表示不确定
		LPASF_FaceDataInfo faceDataInfoList;	// 多张人脸信息
	}ASF_MultiFaceInfo, *LPASF_MultiFaceInfo;

	// 激活文件信息
	typedef struct {
		MPChar startTime;		//开始时间
		MPChar endTime;			//截止时间
		MPChar activeKey;		//激活码
		MPChar platform;		//平台
		MPChar sdkType;			//sdk类型
		MPChar appId;			//APPID
		MPChar sdkKey;			//SDKKEY
		MPChar sdkVersion;		//SDK版本号
		MPChar fileVersion;		//激活文件版本号
	}ASF_ActiveFileInfo, *LPASF_ActiveFileInfo;

	/*******************************************************************************************
	* 获取激活文件信息接口
	*******************************************************************************************/
	MRESULT ASFGetActiveFileInfo(
		LPASF_ActiveFileInfo  activeFileInfo  // [out] 激活文件信息
		);

	/*******************************************************************************************
	* 在线激活接口
	*******************************************************************************************/
	MRESULT ASFOnlineActivation(
		MPChar				AppId,			// [in]  APPID	官网下载
		MPChar				SDKKey,			// [in]  SDKKEY	官网下载
		MPChar				ActiveKey		// [in]  ActiveKey	官网下载
		);

	/*******************************************************************************************
	* 获取设备信息接口
	*******************************************************************************************/
	MRESULT ASFGetActiveDeviceInfo(
		MPChar*				deviceInfo		// [out] 采集的设备信息，用于到开发者中心做离线激活，生成离线授权文件
		);

	/*******************************************************************************************
	* 离线激活接口
	*******************************************************************************************/
	MRESULT ASFOfflineActivation(
		MPChar				filePath		// [in]  许可文件路径(离线授权文件)，需要读写权限
		);

	/************************************************************************
	* 初始化引擎
	************************************************************************/
	MRESULT ASFInitEngine(
		ASF_DetectMode		detectMode,					// [in]	AF_DETECT_MODE_VIDEO 视频模式：适用于摄像头预览，视频文件识别
														//		AF_DETECT_MODE_IMAGE 图片模式：适用于静态图片的识别
		ASF_OrientPriority	detectFaceOrientPriority,	// [in]	检测脸部的角度优先值，参考 ArcFaceCompare_OrientPriority
		MInt32				detectFaceMaxNum,			// [in] 最大需要检测的人脸个数
		MInt32				combinedMask,				// [in] 用户选择需要检测的功能组合，可单个或多个
		MHandle*			hEngine						// [out] 初始化返回的引擎handle
		);

	/************************************************************************
	* 取值范围[0-1]， 默认阈值:0.8， 用户可以根据实际需求，设置遮挡范围
	************************************************************************/
	MRESULT ASFSetFaceShelterParam(
		MHandle hEngine,					// [in] 引擎handle
		MFloat  ShelterThreshhold			// [in] 遮挡阈值
		);

	/******************************************************
	* VIDEO模式:人脸追踪 IMAGE模式:人脸检测
	******************************************************/
	MRESULT ASFDetectFaces(
		MHandle				hEngine,							// [in] 引擎handle
		MInt32				width,								// [in] 图片宽度
		MInt32				height,								// [in] 图片高度
		MInt32				format,								// [in] 颜色空间格式
		MUInt8*				imgData,							// [in] 图片数据
		LPASF_MultiFaceInfo	detectedFaces,						// [out]检测到的人脸信息 
		ASF_DetectModel		detectModel = ASF_DETECT_MODEL_RGB	// [in] 预留字段，当前版本使用默认参数即可
		);

	/******************************************************
	* VIDEO模式:人脸追踪 IMAGE模式:人脸检测
	* 图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好
	******************************************************/
	MRESULT ASFDetectFacesEx(
		MHandle				hEngine,							// [in] 引擎handle
		LPASF_ImageData		imgData,							// [in] 图片数据
		LPASF_MultiFaceInfo	detectedFaces,						// [out] 检测到的人脸信息
		ASF_DetectModel		detectModel = ASF_DETECT_MODEL_RGB	// [in]	预留字段，当前版本使用默认参数即可
		);

	/******************************************************
	* 传入修改后的人脸框，更新人脸信息，用于做双目对齐或其他策略
	* 注意：LPASF_MultiFaceInfo在该接口中既是入参也是出参
	******************************************************/
	MRESULT ASFUpdateFaceData(
		MHandle				hEngine,				// [in] 引擎handle
		MInt32				width, 					// [in] 图片宽度
		MInt32				height, 				// [in] 图片高度
		MInt32				format,					// [in] 颜色空间格式
		MUInt8 *			imgData, 				// [in] 图片数据
		LPASF_MultiFaceInfo detectedFaces			// [in/out]检测到的人脸信息 
		);

	/******************************************************
	* 传入修改后的人脸框，更新人脸信息，用于做双目对齐或其他策略
	* 注意：LPASF_MultiFaceInfo在该接口中既是入参也是出参
	* 图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好
	******************************************************/
	MRESULT ASFUpdateFaceDataEx(
		MHandle                 hEngine,							// [in] 引擎handle
		LPASF_ImageData		    imgData,							// [in] 图像数据
		LPASF_MultiFaceInfo	    detectedFaces						// [in/out] 检测到的人脸信息
		);

	/******************************************************
	* 图像质量检测，（RGB模式： 识别阈值：0.49 注册阈值：0.63  口罩模式：识别阈值：0.29）
	******************************************************/
	MRESULT ASFImageQualityDetect(
		MHandle					hEngine,							// [in] 引擎handle
		MInt32					width,								// [in] 图片宽度
		MInt32					height,								// [in] 图片高度
		MInt32					format,								// [in] 颜色空间格式
		MUInt8 *				imgData,							// [in] 图片数据
		LPASF_SingleFaceInfo	faceInfo,							// [in] 人脸位置信息 
		MInt32					isMask,								// [in] 仅支持传入1、0、-1，戴口罩 1，否则认为未戴口罩
		MFloat*					confidenceLevel,					// [out] 图像质量检测结果
		ASF_DetectModel			detectModel = ASF_DETECT_MODEL_RGB	// [in]	预留字段，当前版本使用默认参数即可
		);

	/******************************************************
	* 图像质量检测，（RGB模式： 识别阈值：0.49 注册阈值：0.63  口罩模式：识别阈值：0.29）
	* 图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好
	******************************************************/
	MRESULT ASFImageQualityDetectEx(
		MHandle					hEngine,							// [in] 引擎handle
		LPASF_ImageData			imgData,							// [in] 图片数据
		LPASF_SingleFaceInfo	faceInfo,							// [in] 人脸位置信息 
		MInt32					isMask,                             // [in] 仅支持传入1、0、-1，戴口罩 1，否则认为未戴口罩
		MFloat*              	confidenceLevel,					// [out] 图像质量检测结果
		ASF_DetectModel			detectModel = ASF_DETECT_MODEL_RGB	// [in]	预留字段，当前版本使用默认参数即可
		);

	/************************************************************************
	* 年龄/性别/人脸3D角度/口罩/遮挡/额头区域（该接口仅支持RGB图像），最多支持4张人脸信息检测，超过部分返回未知
	* RGB活体仅支持单人脸检测，该接口不支持检测IR活体
	************************************************************************/
	MRESULT ASFProcess(
		MHandle				hEngine,			// [in] 引擎handle
		MInt32				width,				// [in] 图片宽度
		MInt32				height,				// [in] 图片高度
		MInt32				format,				// [in] 颜色空间格式
		MUInt8*				imgData,			// [in] 图片数据
		LPASF_MultiFaceInfo	detectedFaces,		// [in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。
		MInt32				combinedMask		// [in] 只支持初始化时候指定需要检测的功能，在process时进一步在这个已经指定的功能集中继续筛选
												//      例如初始化的时候指定检测年龄和性别，在process的时候可以只检测年龄，但是不能检测除年龄和性别之外的功能    
		);

	/************************************************************************
	* 年龄/性别/人脸3D角度/口罩/遮挡/额头区域（该接口仅支持RGB图像），最多支持4张人脸信息检测，超过部分返回未知
	* RGB活体仅支持单人脸检测，该接口不支持检测IR活体
	* 图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好
	************************************************************************/
	MRESULT ASFProcessEx(
		MHandle				hEngine,			// [in] 引擎handle
		LPASF_ImageData		imgData,			// [in] 图片数据
		LPASF_MultiFaceInfo detectedFaces,		// [in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。
		MInt32				combinedMask		// [in] 只支持初始化时候指定需要检测的功能，在process时进一步在这个已经指定的功能集中继续筛选
		//      例如初始化的时候指定检测年龄和性别，在process的时候可以只检测年龄，但是不能检测除年龄和性别之外的功能 
		);

	/************************************************************************
	* 该接口目前仅支持单人脸IR活体检测，默认取第一张人脸
	************************************************************************/
	MRESULT ASFProcess_IR(
		MHandle				hEngine,			// [in] 引擎handle
		MInt32				width,				// [in] 图片宽度
		MInt32				height,				// [in] 图片高度
		MInt32				format,				// [in] 颜色空间格式
		MUInt8*				imgData,			// [in] 图片数据
		LPASF_MultiFaceInfo	detectedFaces,		// [in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。
		MInt32				combinedMask		// [in] 目前只支持传入ASF_IR_LIVENESS属性的传入，且初始化接口需要传入 
		);

	/************************************************************************
	* 该接口目前仅支持单人脸IR活体检测，默认取第一张人脸
	* 图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好
	************************************************************************/
	MRESULT ASFProcessEx_IR(
		MHandle				hEngine,			// [in] 引擎handle
		LPASF_ImageData		imgData,			// [in] 图片数据
		LPASF_MultiFaceInfo detectedFaces,		// [in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。
		MInt32				combinedMask		// [in] 目前只支持传入ASF_IR_LIVENESS属性的传入，且初始化接口需要传入
		);

	//******************************** 人脸识别相关 *********************************************
	typedef struct {
		MByte*		feature;		// 人脸特征信息
		MInt32		featureSize;	// 人脸特征信息长度	
	}ASF_FaceFeature, *LPASF_FaceFeature;

	/************************************************************************
	* 单人脸特征提取
	************************************************************************/
	MRESULT ASFFaceFeatureExtract(
		MHandle					hEngine,							// [in]	引擎handle
		MInt32					width,								// [in] 图片宽度
		MInt32					height,								// [in] 图片高度
		MInt32					format,								// [in] 颜色空间格式
		MUInt8*					imgData,							// [in] 图片数据
		LPASF_SingleFaceInfo	faceInfo,							// [in] 单张人脸位置和角度信息
		ASF_RegisterOrNot		registerOrNot,						// [in] 注册 1 识别为 0
		MInt32					mask,								// [in] 带口罩 1，否则0
		LPASF_FaceFeature		feature								// [out] 人脸特征
		);

	/************************************************************************
	* 单人脸特征提取
	* 图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好
	************************************************************************/
	MRESULT ASFFaceFeatureExtractEx(
		MHandle					hEngine,							// [in]	引擎handle
		LPASF_ImageData			imgData,							// [in] 图像数据
		LPASF_SingleFaceInfo	faceInfo,							// [in] 单张人脸位置和角度信息
		ASF_RegisterOrNot		registerOrNot,						// [in] 注册 1 识别为 0
		MInt32					mask,								// [in] 带口罩 1，否则0
		LPASF_FaceFeature		feature								// [out] 人脸特征
		);

	/************************************************************************
	* 人脸特征比对，推荐阈值 ASF_LIFE_PHOTO：0.80  ASF_ID_PHOTO：0.82
	************************************************************************/
	MRESULT ASFFaceFeatureCompare(
		MHandle					hEngine,						// [in] 引擎handle
		LPASF_FaceFeature		feature1,						// [in] 待比较人脸特征1
		LPASF_FaceFeature		feature2,						// [in] 待比较人脸特征2
		MFloat*					confidenceLevel,				// [out] 比较结果，置信度数值
		ASF_CompareModel		compareModel = ASF_LIFE_PHOTO	// [in] ASF_LIFE_PHOTO：用于生活照之间的特征比对
																//		ASF_ID_PHOTO：用于证件照或证件照和生活照之间的特征比对
		);

	//******************************** 年龄相关 **********************************************
	typedef struct {
		MInt32*	ageArray;				// "0" 代表不确定，大于0的数值代表检测出来的年龄结果
		MInt32	num;					// 检测的人脸个数
	}ASF_AgeInfo, *LPASF_AgeInfo;

	/************************************************************************
	* 获取年龄信息
	************************************************************************/
	MRESULT ASFGetAge(
		MHandle hEngine,				// [in] 引擎handle
		LPASF_AgeInfo ageInfo			// [out] 检测到的年龄信息
		);

	//******************************** 性别相关 **********************************************
	typedef struct {
		MInt32*	genderArray;			// "0" 表示 男性, "1" 表示 女性, "-1" 表示不确定
		MInt32	num;					// 检测的人脸个数	
	}ASF_GenderInfo, *LPASF_GenderInfo;

	/************************************************************************
	* 获取性别信息
	************************************************************************/
	MRESULT ASFGetGender(
		MHandle hEngine,				// [in] 引擎handle
		LPASF_GenderInfo genderInfo		// [out] 检测到的性别信息
		);

	//******************************** 人脸3D 角度信息 ***********************************
	typedef struct {
		MFloat* roll;
		MFloat* yaw;
		MFloat* pitch;
		MInt32 num;
	}ASF_Face3DAngle, *LPASF_Face3DAngle;

	/************************************************************************
	* 获取3D角度信息
	************************************************************************/
	MRESULT ASFGetFace3DAngle(
		MHandle hEngine,					// [in] 引擎handle
		LPASF_Face3DAngle p3DAngleInfo		// [out] 检测到脸部3D 角度信息
		);

	//******************************** 活体信息 ***********************************
	typedef struct {
		MFloat		thresholdmodel_BGR;
		MFloat		thresholdmodel_IR;
	}ASF_LivenessThreshold, *LPASF_LivenessThreshold;

	/************************************************************************
	* 取值范围[0-1]，默认值 BGR:0.5 IR:0.7， 用户可以根据实际需求，设置不同的阈值
	************************************************************************/
	MRESULT ASFSetLivenessParam(
		MHandle					hEngine,		// [in] 引擎handle
		LPASF_LivenessThreshold threshold		// [in] 活体置信度
		);

	typedef struct {
		MInt32*	isLive;			// [out] 判断是否真人， 0：非真人；
		//						1：真人；
		//						-1：不确定； 
		//						-2:传入人脸数>1；
		//                      -3: 人脸过小
		//                      -4: 角度过大
		//                      -5: 人脸超出边界 
		//					    -6: 深度图错误
		//					    -7: 红外图太亮了
		MInt32 num;
	}ASF_LivenessInfo, *LPASF_LivenessInfo;

	/************************************************************************
	* 获取RGB活体结果
	************************************************************************/
	MRESULT ASFGetLivenessScore(
		MHandle hEngine,					// [in] 引擎handle
		LPASF_LivenessInfo livenessInfo		// [out] 检测RGB活体结果
		);

	/************************************************************************
	* 获取IR活体结果
	************************************************************************/
	MRESULT ASFGetLivenessScore_IR(
		MHandle				hEngine,			// [in] 引擎handle
		LPASF_LivenessInfo	 irLivenessInfo		// [out] 检测到IR活体结果
		);

	//******************************** 口罩检测相关 **********************************************
	typedef struct
	{
		MInt32*	maskArray;				// "0" 代表没有带口罩，"1"代表带口罩 ,"-1"表不确定
		MInt32	num;					// 检测的人脸个数
	}ASF_MaskInfo, *LPASF_MaskInfo;

	/************************************************************************
	* 获取口罩检测的结果
	************************************************************************/
	MRESULT ASFGetMask(
		MHandle hEngine,				// [in] 引擎handle
		LPASF_MaskInfo maskInfo			// [out] 检测到的口罩检测相关
		);

	//******************************** 额头区域检测相关 **********************************************

	#define LANDMARKS_NUM 4	//特征点个数

	typedef struct
	{
		MFloat x;
		MFloat y;
	}ASF_FaceLandmark, *LPASF_FaceLandmark;

	typedef struct
	{
		ASF_FaceLandmark *point;		//额头点位
		MInt32 num;						//人脸数量
	}ASF_LandMarkInfo, *LPASF_LandMarkInfo;

	/************************************************************************
	* 获取额头区域检测结果（当前只支持0, 90, 180, 270度角检测）
	************************************************************************/
	MRESULT ASFGetFaceLandMark(
		MHandle				engine,			 // [in] 引擎handle
		LPASF_LandMarkInfo  LandMarkInfo	 // [out]人脸额头点数组，每张人脸额头区域通过四个点表示
		);

	/************************************************************************
	* 销毁引擎
	************************************************************************/
	MRESULT ASFUninitEngine(
		MHandle hEngine
		);

	/************************************************************************
	* 获取版本信息
	************************************************************************/
	const ASF_VERSION ASFGetVersion();

#ifdef __cplusplus
}
#endif
#endif