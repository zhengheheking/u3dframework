package utils;

import java.util.concurrent.ArrayBlockingQueue;

public class JkSet<T> {
	private Object[] _values;
	public int capacity;

	private ArrayBlockingQueue<Integer> indexList;

	public JkSet(int capacity) {
		this.capacity = capacity;
		indexList = new ArrayBlockingQueue<Integer>(capacity);
		for (int i = 0; i < capacity; i++) {
			indexList.add(i);
		}

		_values = new Object[capacity];
	}

	@SuppressWarnings("unchecked")
    public T get(int index) {
		Object valObject = _values[index];
		if (valObject == null)
			return null;
		return (T)valObject;

	}

	public int add(T client) {
		if (isFull())
			return -1;
		int index = indexList.poll();
		_values[index] = client;
		return index;
	}

	public void remove(int index) {

		indexList.add(index);
		_values[index] = null;

	}

	public boolean isFull() {
		return indexList.isEmpty();
	}

	public int size() {
		return capacity - indexList.size();
	}

}
