from sqlalchemy import Column, Integer, String, ForeignKey, Date
from app.database.connection import Base

class ChapterSchema(Base):
    id = Column(Integer, primary_key=True)
    manga_id = Column(Integer, ForeignKey)
    title = Column(String)
    number = Column(Integer)
    release_date = Column(Date)
