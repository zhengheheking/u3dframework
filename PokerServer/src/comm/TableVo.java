package comm;

public class TableVo {
	public byte sqlMode;
	public void initForAdd(ObjectVo vo) {
		sqlMode = SqlBase.ADD_MODE;
	}
	public void initForUpdate(ObjectVo vo) {
		sqlMode = SqlBase.UPDATE_MODE;
	}
	public void initForDel(ObjectVo vo) {
		sqlMode = SqlBase.DELETE_MODE;
	}
}
