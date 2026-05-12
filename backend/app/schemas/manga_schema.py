from sqlalchemy import Column, Integer, String, Date, ForeignKey
from app.database.connection import Base

class MangaSchema(Base):
    id = Column(Integer, primary_key=True)
    title = Column(String)
    status = Column(String)
    description = Column(String(500))
    release_date = Column(Date)
    thumbnail_url = Column(String, nullable=True)
    #authornames
    #genrenames