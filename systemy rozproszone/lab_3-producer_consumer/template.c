#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <signal.h>
#include "pqueue.h"

char *filename = "queue.dat";

int itemId = 0;

typedef struct item item;
struct item {
	int id;
};

// funkcja produkuj¹ca losowe elementy i zwracaj¹ca wskaŸnik do nich. Ka¿dy element ma unikalne ID.
item* produce() {
	int time = rand() % 5 + 1;
	item *it = (item *)malloc(sizeof(item));
	
	sleep(time);
	it->id = itemId;
	printf("produkuje %i\n", itemId);
	itemId += 1;
	return it;
}
//consume(item *it): funkcja konsumuj¹ca element o podanym wskaŸniku it i zwalniaj¹ca pamiêæ zaalokowan¹ dla tego elementu.
void consume(item *it) {
	printf("konsumuje %i\n", it->id);
	int time = rand() % 5 + 1;
	sleep(time);
	free(it);
}

// funkcja produkuj¹ca elementy i umieszczaj¹ca je w kolejce. Wywo³anie funkcji qunserialize() odtwarza kolejkê z pliku, a qserialize() zapisuje kolejkê do pliku po ka¿dym wykonaniu operacji na niej.
void producer(pid_t childPid) {
	pqueue* queue;
	
	for (int i = 0; i<16; i+=1){
		qunserialize(&queue, sizeof(item), filename);
		qinsert(&queue, produce(), 2);
		qserialize(queue, sizeof(item), filename);
	}
}
// funkcja konsumuj¹ca elementy z kolejki. Dopóki kolejka jest pusta, funkcja odczytuje j¹ z pliku przy u¿yciu
void consumer() {
	pqueue* queue;

	for (int i = 0; i<16; i+=1){
		pqueue *queue = NULL;
		while(queue ==NULL){

			qunserialize(&queue, sizeof(item), filename);
			sleep(1);
		}

		item* item_c = queue->data;
		queue = qpop(&queue);
		qserialize(queue, sizeof(item), filename);
		consume(item_c);
	}
}

int main(int argc, char **argv) {

	pid_t pid;
	pqueue *qu = NULL;
	/* watch -n 1 ps -l --forest */
	
	/* create empty queue */
	qserialize(qu, sizeof(item), filename);

	pid = fork();
	printf("PID = %i\n", pid);

	if(pid == 0){
		printf("started consumer??\n");
		consumer();
		return  0;
	}
	else {
		printf("started producer??\n");
		producer(pid);
		return  0;
	}	

	return 0;
}

