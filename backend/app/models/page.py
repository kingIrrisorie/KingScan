from sqlalchemy import Column, Integer, ForeignKey
from app.database.connection import Base

class Page(Base):
    __tablename__ = "page"

    id = Column(Integer, primary_key=True)
    chapter_id = (Integer, ForeignKey)
    page_number = (Integer)
    # images 