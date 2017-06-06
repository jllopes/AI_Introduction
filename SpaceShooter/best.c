#include <stdio.h>
#include <stdlib.h>
#include <string.h>



double best[52],worst[52],mean[52],desv[52];
double bestl[52],worstl[52],meanl[52],desvl[52];
double melhorALL,desvbest;
int indexbest;

int main(int argc, char *argv[]) {
	// your code goes here
	char* filename=(char*)malloc(200*sizeof(char*));;
	char* line = (char*)malloc(100*sizeof(char*));
				FILE * fp;
    size_t len = 0;
    ssize_t read;
    int i;
	for(i = 1;i<11;i++){
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
			if(j==50){
				if(melhorALL < best[j]){
					indexbest = i;
					desvbest = desv[j]; 

					melhorALL = best[j];

				}
			}
	 	}

   	fclose(fp);
	}

	FILE *f_w;
		snprintf(filename, 200, "C:\\Users\\Gabriel\\Documents\\ProjetoIIA_3\\TP3\\bestofbest.txt");

	f_w = fopen(filename,"a");
		fprintf(f_w,"%s\t%d\t%lf\t%lf\t\n",argv[1],indexbest,melhorALL,desvbest);
	
	fclose(f_w);
	return 0;
}
