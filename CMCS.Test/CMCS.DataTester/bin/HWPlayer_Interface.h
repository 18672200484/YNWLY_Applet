#pragma  once

#ifdef HWPLAYERAPI
#else
#define HWPLAYERAPI extern "C" __declspec(dllimport)
#endif

#define INVALID_VIDEOHANLE NULL
typedef void * PVideoHandle;
typedef enum tag_VIDEOFORMAT
{
	RGB_FORMAT = 1,
	H264_FORMAT = 2,
	YUV_FORMAT = 3
}VIDEOFORMAT;

//�ӿں�������
/*********************************************************************************
������HWPlayer_Init
��������ʼ��
������
	bShowVideo falseΪ����ʾ,trueΪ��ʾ��Ĭ��Ϊ����ʾ�����ò���Ϊfalseʱ�����ô�
	����HWPlayer_SetPlayWnd��Ч�����Ҳ�֧��RGB_FORMAT��YUV_FORMAT��ʽ���������

����ֵ��
	true ��ʼ���ɹ���false��ʼ��ʧ��
**********************************************************************************/

HWPLAYERAPI bool WINAPI HWPlayer_Init(bool bShowVideo);

/*********************************************************************************
������HWPlayer_Open
����������Ƶ��
������
chVideoFileName ��Ƶ�ļ����ƣ������������������rtsp��ַ���������Ƶ�ļ�����������Ƶ�ļ�����
wVideoType ��Ƶ���ͣ�1��ʾ������Ƶ����2��ʾ�ļ���Ƶ��

����ֵ����Ƶ����������ִ��ʧ�ܣ��򷵻�INVALID_VIDEOHANLE
**********************************************************************************/

HWPLAYERAPI PVideoHandle WINAPI HWPlayer_Open(char *chVideoFileName,WORD wVideoType);

/*********************************************************************************
������HWPlayer_SetPlayWnd
���������ò�����Ƶ������
������
hVideoHandle Ҫ��ʾ����Ƶ�����
hPlayWnd ��ʾ��Ƶ�����������ΪNULL������ʾ

����ֵ��true��ʾ���óɹ���false��ʾ����ʧ��
**********************************************************************************/

HWPLAYERAPI bool WINAPI HWPlayer_SetPlayWnd(PVideoHandle hVideoHandle,HWND hPlayWnd);

/*********************************************************************************
������HWPlayer_Pause
������������Ƶ����ͣ������
������
hVideoHandle Ҫ���Ƶ���Ƶ�����
bPause true��ʾ��ͣ��false��ʾ����

����ֵ��true��ʾ���óɹ���false��ʾ����ʧ��
**********************************************************************************/

HWPLAYERAPI bool WINAPI HWPlayer_Pause(PVideoHandle hVideoHandle,bool bPause);

/*********************************************************************************
������HWPlayer_Close
�������ر���Ƶ���ջ�ֹͣ����
������
hVideoHandle Ҫ���Ƶ���Ƶ�����
����ֵ��true��ʾ���óɹ���false��ʾ����ʧ��
**********************************************************************************/

HWPLAYERAPI bool WINAPI HWPlayer_Close(PVideoHandle hVideoHandle);

/*********************************************************************************
������RealStreamCallBack
��������ȡʵʱ��Ƶ���Ļص�����
������
pUserData �û����ûص�����ʱ��������û�����
hVideoHandle ��Ƶ����� 
pBuffer ��Ƶ����
nSize   ��Ƶ���ݳ���
����ֵ����
**********************************************************************************/

typedef void(CALLBACK *RealStreamCallBack)(void *pUserData,PVideoHandle hVideoHandle,BYTE *pBuffer,UINT nSize);
/*********************************************************************************
������HWPlayer_SetCallback
������������Ƶ���յĻص�����
������
hVideoHandle Ҫ��Ӧ����Ƶ�����
pUserData   �����û����ݣ��ص�ʱ��Ϊ��һ����������
fucCallback ת�����ݻص�����
fucCallbackH264 264���ݻص�����
����ֵ��true��ʾ���óɹ���false��ʾ����ʧ��
**********************************************************************************/

HWPLAYERAPI bool WINAPI HWPlayer_SetCallback(PVideoHandle hVideoHandle,void *pUserData,VIDEOFORMAT videoFormat,RealStreamCallBack fucCallback,RealStreamCallBack fucCallbackH264);

/*********************************************************************************
������HWPlayer_Quit
�������˳�����ʱ���ͷ�������Դ
������
�޲���
����ֵ���޷���ֵ
**********************************************************************************/

HWPLAYERAPI void WINAPI HWPlayer_Quit();

/*********************************************************************************
������ConvertH264ToAvi
��������264�ļ�תΪAVI
������
chH264File  264�ļ�
chAVIFile   avi�ļ�
nFrameRate
����ֵ��false��ʾʧ�ܡ�true��ʾ�ɹ�
**********************************************************************************/

HWPLAYERAPI bool WINAPI ConvertH264ToAvi(char *chH264File,char * chAVIFile,int nFrameRate = 15);


/*********************************************************************************
������GetFrameRate
��������ȡ֡��
������
hVideoHandle Ҫ��ȡ����Ƶ�����
fFrameRate  ����֡��
����ֵ��false��ʾʧ�ܡ�true��ʾ�ɹ�

**********************************************************************************/

HWPLAYERAPI bool WINAPI GetFrameRate(PVideoHandle hVideoHandle,float *fFrameRate);

/*********************************************************************************
������CaptureImage
��������ȡͼ��ĳߴ�
������
hVideoHandle Ҫ��ȡ����Ƶ�����
nImageWidth  ͼ����
nImageHeight ͼ��߶�
����ֵ��false��ʾʧ�ܡ�true��ʾ�ɹ�

**********************************************************************************/

HWPLAYERAPI bool WINAPI GetImageSize(PVideoHandle hVideoHandle,unsigned int *nImageWidth,unsigned int *nImageHeight);
/*********************************************************************************
������CaptureImage
������ץ��ͼ��
������
hVideoHandle Ҫ��ȡ����Ƶ�����
pImageData  һ֡ͼ�����ݣ���ʽ��RGB
����ֵ��false��ʾʧ�ܡ�true��ʾ�ɹ�

**********************************************************************************/

HWPLAYERAPI bool WINAPI CaptureImage(PVideoHandle hVideoHandle,unsigned char *pImageData);
