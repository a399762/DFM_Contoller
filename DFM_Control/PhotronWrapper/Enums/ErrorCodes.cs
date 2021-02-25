using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotronWrapper.Enums
{
    public enum ErrorCodes
    {
         PDC_ERROR_NOERROR          =	1,
         PDC_ERROR_UNINITIALIZE	    =   2,
         PDC_ERROR_ILLEGAL_DEV_NO	=   3,	
         PDC_ERROR_ILLEGAL_CHILD_NO =	4,	
         PDC_ERROR_ILLEGAL_VALUE	=	5,
         PDC_ERROR_ALLOCATE_FAILED  =	6,	
         PDC_ERROR_INITIALIZED		=   7,	
         PDC_ERROR_NO_DEVICE		=	8,	
         PDC_ERROR_TIMEOUT			=   9,	
         PDC_ERROR_FUNCTION_FAILED  =   10,	
         PDC_ERROR_FUNCTION_DISABLE =	11,	
         PDC_ERROR_NO_DATA		    =	12,
         PDC_ERROR_UNKNOWN_FRAME    =	13,	
         PDC_ERROR_CAMERAMODE	    =  	14,	
         PDC_ERROR_NO_ENDLESS	    =	15,	
         PDC_ERROR_FILEREAD_FAILED	=   16,	
         PDC_ERROR_FILEWRITE_FAILED	=   17,	
         PDC_ERROR_IMAGE_SIZEOVER	=   18,	
         PDC_ERROR_FRAME_AREAOVER	=   19,	
         PDC_ERROR_PLAYMODE		    =	20,	
         PDC_ERROR_NOT_SUPPORTED    =	21,	
         PDC_ERROR_DROP_FRAME	    =	22,
         PDC_ERROR_FILE_OPEN_ALREADY=	23,
         PDC_ERROR_FILE_NOTOPEN	    =	24,	
         PDC_ERROR_CONVERSION_OF_STRING	=25,

         PDC_ERROR_LOAD_FAILED      =   100,	
         PDC_ERROR_FREE_FAILED      =	101,	
         PDC_ERROR_LOADED			=   102,	
         PDC_ERROR_NOTLOADED		=	103,
         PDC_ERROR_UNDETECTED		=   104,	
         PDC_ERROR_OVER_DEVICE		=   105,	
         PDC_ERROR_INIT_FAILED		=   106,	
         PDC_ERROR_OPEN_ALREADY		=   107,
         PDC_ERROR_NOTOPEN			=   108,	
         PDC_ERROR_LIVEONLY			=   110,	
         PDC_ERROR_PLAYBACKONLY		=   111,	
         PDC_ERROR_FASTDRIVE_PLAYBACKONLY	=   112,	
         PDC_ERROR_NOTCONNECTDEVICE =   113,	
         PDC_ERROR_ICMP_TIMEOUT		=   114,

         PDC_ERROR_SEND_ERROR		=   200,	
         PDC_ERROR_RECEIVE_ERROR	=	201,
         PDC_ERROR_CLEAR_ERROR		=   202,	
         PDC_ERROR_COMMAND_ERROR	=	203,	

         PDC_ERROR_COMPARE_DATA_ERROR   =	204,	

         PDC_ERROR_ENABLE_CONFIG_ERROR	=   300,
         PDC_ERROR_REG_ACCESS_ERROR	    =   301,	

         PDC_ERROR_DEVICE_ACCESS_FAILED	=   400,	
         PDC_ERROR_UNUSUAL_DATA			=   401, 

         PDC_ERROR_STORAGE_NO_CARD		=   402,	
         PDC_ERROR_STORAGE_WRITE		=	403,
         PDC_ERROR_STORAGE_READ			=   404,
         PDC_ERROR_STORAGE_SIZE			=   405,
         PDC_ERROR_STORAGE_FORMAT		=   406,
         PDC_ERROR_STORAGE_LOCKED_PASSWORD	=   407,
         PDC_ERROR_STORAGE_INVALID_PASSWORD	=   408,
         PDC_ERROR_STORAGE_DOWNLOADING	    =   409,

         PDC_ERROR_DETECTDEVICE_WITH_TX =  500, 
         PDC_ERROR_PARTITION_LOCKED		=   501,
         PDC_ERROR_INITIALERROR			=   502,
    };
}
