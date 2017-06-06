#include <stdio.h>
#include <stdlib.h>
#include <string.h>



double best[52],worst[52],mean[52],desv[52];
double bestl[52],worstl[52],meanl[52],desvl[52];


int main(int argc, char *argv[]) {
	// your code goes here
	char* filename=(char*)malloc(200*sizeof(char*));;
	char* line = (char*)malloc(100*sizeof(char*));
				FILE * fp;
    size_t len = 0;
    ssize_t read;
	for(int i = 1;i<11;i++){
		char* line = (char*)malloc(100*sizeof(char*));

		snprintf(filename, 200, "C:\\Users\\Gabriel\\Documents\\ProjetoIIA_3\\TP3\\testebase\\%s\\teste+%d+.txt", argv[1],i);
	 	fp=fopen(filename,"r+");
	 	if(fp== NULL){
	 		printf("deosnt extis %s\n",filename);
	 		break;
	 	}

	 	int temp;

	 	for(int j = 0;j<51;j++){
	 		fscanf(fp,"%d,%lf,%lf,%lf,%lf",&temp,&best[j],&worst[j],&mean[j],&desv[j]);//save line
	 		//update lines
	 		bestl[j]+=best[j];
			worstl[j]+=worst[j];
			meanl[j]+=mean[j];
			desvl[j]+=desv[j];
	 	}
   	fclose(fp);
	}

	FILE *f_w;
		snprintf(filename, 200, "C:\\Users\\Gabriel\\Documents\\ProjetoIIA_3\\TP3\\testebase\\%s\\results.xls", argv[1]);

	f_w = fopen(filename,"a");
	fprintf(f_w,"BEST\tWORST\tMEAN\tDESV\t%s\n",argv[1]);
	for(int i=0;i<51;i++){
		bestl[i]/=10;
		worstl[i]/=10;
		meanl[i]/=10;
		desvl[i]/=10;
		fprintf(f_w,"%lf\t%lf\t%lf\t%lf\t\n",bestl[i],worstl[i],meanl[i],desvl[i]);
	}
	fclose(f_w);
	return 0;
}
